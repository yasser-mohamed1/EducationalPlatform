using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using EducationalPlatform.Repositories;
using EducationalPlatform.Services;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace EducationalPlatform.Tests.Services
{
    [TestFixture]
    public class TeacherAccountServiceTests
    {
        private Mock<ITeacherAccountRepository> _mockRepo;
        private TeacherAccountService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<ITeacherAccountRepository>();
            _service = new TeacherAccountService(_mockRepo.Object);
        }

        [Test]
        public async Task RegisterTeacherAsync_ExistingEmail_ReturnsError()
        {
            // Arrange
            var dto = new RegisterTeacherDto { Email = "exists@test.com" };
            _mockRepo.Setup(r => r.IsEmailTakenAsync(dto.Email)).ReturnsAsync(true);

            // Act
            var result = await _service.RegisterTeacherAsync(dto);

            // Assert
            Assert.That(result, Does.Contain("already registered"));
        }

        [Test]
        public async Task RegisterTeacherAsync_WeakPassword_ReturnsError()
        {
            // Arrange
            var dto = new RegisterTeacherDto { Password = "123" };
            _mockRepo.Setup(r => r.CreateUserAsync(It.IsAny<ApplicationUser>(), dto.Password))
                    .ReturnsAsync(IdentityResult.Failed(new IdentityError
                    {
                        Description = "Password too weak"
                    }));

            // Act
            var result = await _service.RegisterTeacherAsync(dto);

            // Assert
            Assert.That(result, Does.Contain("Weak password"));
        }
    }
}