using Donations.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Donations.Common.Test.Fakes
{
    public class FakeUserManager : UserManager<User>
    {
        public FakeUserManager()
            : base(
                  new Mock<IUserStore<User>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<User>>().Object,
                  new IUserValidator<User>[0],
                  new IPasswordValidator<User>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<User>>>().Object
            )
        { }

        public override string GetUserId(ClaimsPrincipal principal)
        {
            return Guid.NewGuid().ToString();
        }

        public override Task<User> FindByEmailAsync(string email)
        {
            return Task.FromResult(new User { Id = Guid.NewGuid(), Email = email });
        }

        public override Task<User> FindByIdAsync(string userId)
        {
            return Task.FromResult(new User { Id = Guid.Parse(userId), Email = "ertanhasani96@gmail.com" });
        }
    }
}
