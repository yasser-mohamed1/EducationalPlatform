using EducationalPlatform.DTO;
using EducationalPlatform.Repositories;
using EducationalPlatform.Services;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NUnit.Framework;
using EducationalPlatform.Entities;

namespace EducationalPlatform.Tests.Services
{
    [TestFixture]
    public class AccountServiceTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IConfiguration> _mockConfig;
        private AccountService _service;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockConfig = new Mock<IConfiguration>();
            _service = new AccountService(_mockUserRepository.Object, _mockConfig.Object);
        }

        [Test]
        public async Task LoginAsync_Success_ReturnsTokenAndUserInfo()
        {
            // Arrange
            var userDto = new LoginUserDto
            {
                userName = "testuser",
                Password = "testpassword"
            };

            var mockUser = new ApplicationUser
            {
                userId = 1,
                UserName = "testuser"
            };

            _mockUserRepository.Setup(r => r.FindByNameAsync(userDto.userName))
                .ReturnsAsync(mockUser);

            _mockUserRepository.Setup(r => r.CheckPasswordAsync(mockUser, userDto.Password))
                .ReturnsAsync(true);

            _mockUserRepository.Setup(r => r.GetRolesAsync(mockUser))
                .ReturnsAsync(new List<string> { "User" });

            _mockConfig.Setup(c => c["JWT:Secret"])
                .Returns("6f6a83********************0dc9cF6,");

            // Act
            var result = await _service.LoginAsync(userDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<LoginResponseDto>(result);
            Assert.That(result.Id, Is.EqualTo(mockUser.userId));
            Assert.That(result.Role, Is.EqualTo("User"));
            Assert.IsNotEmpty(result.Token);
            Assert.That(result.Expiration.ToString(), Is.EqualTo(DateTime.UtcNow.AddHours(1).ToString()));
        }

        [Test]
        public async Task LoginAsync_Failed_InvalidUsername_ReturnsNull()
        {
            // Arrange
            var userDto = new LoginUserDto
            {
                userName = "invaliduser",
                Password = "testpassword"
            };

            _mockUserRepository.Setup(r => r.FindByNameAsync(userDto.userName))
                .ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _service.LoginAsync(userDto);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task LoginAsync_Failed_InvalidPassword_ReturnsNull()
        {
            // Arrange
            var userDto = new LoginUserDto
            {
                userName = "testuser",
                Password = "wrongpassword"
            };

            var mockUser = new ApplicationUser
            {
                userId = 1,
                UserName = "testuser"
            };

            _mockUserRepository.Setup(r => r.FindByNameAsync(userDto.userName))
                .ReturnsAsync(mockUser);

            _mockUserRepository.Setup(r => r.CheckPasswordAsync(mockUser, userDto.Password))
                .ReturnsAsync(false);

            // Act
            var result = await _service.LoginAsync(userDto);

            // Assert
            Assert.IsNull(result);
        }
    }
}