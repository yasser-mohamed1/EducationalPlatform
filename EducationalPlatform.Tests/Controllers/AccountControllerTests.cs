using EducationalPlatform.Controllers;
using EducationalPlatform.DTO;
using EducationalPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EducationalPlatform.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        private Mock<IAccountService> _mockAccountService;
        private AccountController _controller;

        [SetUp]
        public void Setup()
        {
            _mockAccountService = new Mock<IAccountService>();
            _controller = new AccountController(_mockAccountService.Object);
        }

        [Test]
        public async Task Login_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var loginDto = new LoginUserDto { userName = "", Password = "" };
            _controller.ModelState.AddModelError("userName", "Required");

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task Login_ValidCredentials_ReturnsOkWithToken()
        {
            // Arrange
            var loginDto = new LoginUserDto { userName = "user", Password = "pass" };
            var expectedToken = "fake-jwt-token";
            _mockAccountService.Setup(s => s.LoginAsync(loginDto))
                .ReturnsAsync(expectedToken);

            // Act
            var result = await _controller.Login(loginDto) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(expectedToken, result.Value);
        }

        [Test]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var loginDto = new LoginUserDto { userName = "user", Password = "wrongpass" };
            _mockAccountService.Setup(s => s.LoginAsync(loginDto))
                .ReturnsAsync((string)null);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }
    }
}
