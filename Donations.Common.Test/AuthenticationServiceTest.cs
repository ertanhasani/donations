using Donations.Common.DTOs;
using Donations.Common.Services;
using Donations.Common.Test.Fakes;
using Donations.Data.Interfaces;
using Donations.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Security;
using System.Threading.Tasks;

namespace Donations.Common.Test
{
    public class AuthenticationServiceTest
    {
        private Mock<FakeUserManager> _userManager;
        private Mock<FakeSignInManager> _signInManager;
        private Mock<IDonationsRepository> _repository;
        private Mock<IConfiguration> _configuration;
        private AuthenticationService _authenticationService;
        private readonly Credentials DefaultCredentials = new Credentials { Email = "email@test.com", Password = "some-password" };

        [SetUp]
        public void Setup()
        {
            _userManager = new Mock<FakeUserManager>();
            _signInManager = new Mock<FakeSignInManager>();
            _repository = new Mock<IDonationsRepository>();
            _configuration = new Mock<IConfiguration>();

            _authenticationService = new AuthenticationService(_repository.Object, _signInManager.Object, _userManager.Object, _configuration.Object);
        }

        [Test]
        public void LoginThrowsArgumentNullExceptionWhenSendingNullCredentials()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await _authenticationService.Login(null));

            _signInManager.Verify(p => p.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Never);
            _userManager.Verify(p => p.FindByEmailAsync(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void LoginThrowsArgumentExceptionWhenEmailIsNullOrEmpty()
        {
            var credentials = new Credentials { Email = String.Empty, Password = "some-password" };

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await _authenticationService.Login(credentials));

            _signInManager.Verify(p => p.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Never);
            _userManager.Verify(p => p.FindByEmailAsync(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void LoginThrowsArgumentExceptionWhenPasswordIsNullOrEmpty()
        {
            var credentials = new Credentials { Email = "email@test.com", Password = String.Empty };

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await _authenticationService.Login(credentials));

            _signInManager.Verify(p => p.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Never);
            _userManager.Verify(p => p.FindByEmailAsync(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void LoginThrowsSecurityExceptionWhenAuthenticationFails()
        {
            _signInManager.Setup(p => p.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(SignInResult.Failed));

            Assert.ThrowsAsync<SecurityException>(async () =>
                await _authenticationService.Login(DefaultCredentials));

            _signInManager.Verify(p => p.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            _userManager.Verify(p => p.FindByEmailAsync(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task LoginReturnsTokenOnSuccess()
        {
            var user = new User { Id = Guid.NewGuid() };
            var jwtKey = "some-long-jwt-key";

            _signInManager.Setup(p => p.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(SignInResult.Success));

            _userManager.Setup(p => p.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _configuration.Setup(p => p[It.IsAny<string>()])
                .Returns(jwtKey);

            var result = await _authenticationService.Login(DefaultCredentials);

            Assert.IsNotEmpty(result);
            _signInManager.Verify(p => p.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            _userManager.Verify(p => p.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
