using Donations.Common.DTOs;
using Donations.Common.Helpers;
using Donations.Common.Interfaces;
using Donations.Data.Enums;
using Donations.Data.Interfaces;
using Donations.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Donations.Common.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private IDonationsRepository _repository;
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private IConfiguration _configuration;

        public AuthenticationService(IDonationsRepository repository, SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration configuration)
        {
            _repository = repository;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> Login(Credentials credentials)
        {
            Guard.NotNull(credentials, nameof(credentials));
            Guard.StringNotEmpty(credentials.Email, nameof(credentials.Email));
            Guard.StringNotEmpty(credentials.Password, nameof(credentials.Password));

            var result = await _signInManager.PasswordSignInAsync(credentials.Email, credentials.Password, true, false);

            if (!result.Succeeded)
            {
                throw new SecurityException($"Wrong credentials for email: {credentials.Email}");
            }

            var user = await _userManager.FindByEmailAsync(credentials.Email);
            var token = GenerateToken(user.Id);

            return token;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task Register(RegisterUser registerUser)
        {
            Guard.NotNull(registerUser, nameof(registerUser));
            Guard.StringNotEmpty(registerUser.FullName, nameof(registerUser.FullName));
            Guard.StringNotEmpty(registerUser.Email, nameof(registerUser.Email));
            Guard.StringNotEmpty(registerUser.Password, nameof(registerUser.Password));

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = registerUser.FullName,
                Email = registerUser.Email,
                UserName = registerUser.Email
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (!result.Succeeded)
            {
                throw new Exception($"Error: `{JsonConvert.SerializeObject(result.Errors)}` while trying to register user: `{JsonConvert.SerializeObject(registerUser)}`.");
            }

            var roleName = Enum.GetName(typeof(UserRole), UserRole.User);

            var roleResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                throw new Exception($"Error: `{JsonConvert.SerializeObject(roleResult.Errors)}` adding role `{roleName}` to user: '{user.Id}'.");
            }
        }

        private string GenerateToken(Guid userId)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["ApplicationSettings:JwtKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("user_id", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
