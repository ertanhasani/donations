using Donations.API.Controllers;
using Donations.Common.DTOs;
using Donations.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Security;
using System.Threading.Tasks;

namespace Donations.API.Test
{
    public class AuthenticationControllerTest
    {
        private AuthenticationController _authenticationController;
        private Mock<IAuthenticationService> _authenticationService;
        private Mock<ILogger<AuthenticationController>> _logger;
        private readonly Credentials DefaultCredentials = new Credentials { Email = "email@test.com", Password = "some-password" };


        [SetUp]
        public void Setup()
        {
            _authenticationService = new Mock<IAuthenticationService>();
            _logger = new Mock<ILogger<AuthenticationController>>();

            _authenticationController = new AuthenticationController(_authenticationService.Object, _logger.Object);
        }

        [Test]
        public async Task LoginReturns200OkOnSuccess()
        {
            var token = "some-token";

            _authenticationService.Setup(p => p.Login(It.IsAny<Credentials>()))
                .ReturnsAsync(token);

            var result = await _authenticationController.Login(DefaultCredentials);

            Assert.AreEqual(StatusCodes.Status200OK, ((IStatusCodeActionResult)result).StatusCode);

            _authenticationService.Verify();
        }

        [Test]
        public async Task LoginReturns400BadRequestWhenNoBodySent()
        {
            _authenticationService.Setup(p => p.Login(It.IsAny<Credentials>()))
                .ThrowsAsync(new ArgumentException());

            var result = await _authenticationController.Login(null);

            Assert.AreEqual(StatusCodes.Status400BadRequest, ((IStatusCodeActionResult)result).StatusCode);

            _authenticationService.Verify();
        }

        [Test]
        public async Task LoginReturns401UnauthorizedWhenAuthenticationFails()
        {
            _authenticationService.Setup(p => p.Login(It.IsAny<Credentials>()))
                .ThrowsAsync(new SecurityException());

            var result = await _authenticationController.Login(DefaultCredentials);

            Assert.AreEqual(StatusCodes.Status401Unauthorized, ((IStatusCodeActionResult)result).StatusCode);

            _authenticationService.Verify();
        }

        [Test]
        public async Task LoginReturns500InternalServerErrrorOnOtherExceptions()
        {
            _authenticationService.Setup(p => p.Login(It.IsAny<Credentials>()))
                .ThrowsAsync(new Exception());

            var result = await _authenticationController.Login(DefaultCredentials);

            Assert.AreEqual(StatusCodes.Status500InternalServerError, ((IStatusCodeActionResult)result).StatusCode);

            _authenticationService.Verify();
        }
    }
}
