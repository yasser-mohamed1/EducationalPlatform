using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using EducationalPlatform.Repositories;
using EducationalPlatform.Services;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace EducationalPlatform.Tests.Services
{
    [TestFixture]
    public class StudentAccountServiceTests
    {
        private Mock<IStudentAccountRepository> _mockStudentRepository;
        private StudentAccountService _service;

        [SetUp]
        public void Setup()
        {
            _mockStudentRepository = new Mock<IStudentAccountRepository>();
            _service = new StudentAccountService(_mockStudentRepository.Object);
        }

        [Test]
        public async Task RegisterStudentAsync_UsernameAlreadyExists_ReturnsErrorMessage()
        {
            // Arrange
            var dto = CreateValidStudentDto();
            _mockStudentRepository.Setup(r => r.IsUsernameTakenAsync(dto.Username)).ReturnsAsync(true);

            // Act
            var result = await _service.RegisterStudentAsync(dto);

            // Assert
            Assert.AreEqual("Username already exists", result);
        }

        [Test]
        public async Task RegisterStudentAsync_EmailAlreadyRegistered_ReturnsErrorMessage()
        {
            // Arrange
            var dto = CreateValidStudentDto();
            _mockStudentRepository.Setup(r => r.IsEmailTakenAsync(dto.Email)).ReturnsAsync(true);

            // Act
            var result = await _service.RegisterStudentAsync(dto);

            // Assert
            Assert.AreEqual("Email is already registered", result);
        }

        [Test]
        public async Task RegisterStudentAsync_FailedUserCreation_ReturnsErrorMessage()
        {
            // Arrange
            var dto = CreateValidStudentDto();

            IdentityResult identityResult = IdentityResult.Failed(new IdentityError { Description = "Weak password" });

            _mockStudentRepository.Setup(r => r.CreateUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(identityResult);

            // Act
            var result = await _service.RegisterStudentAsync(dto);

            // Assert
            Assert.AreEqual("Weak password", result);
        }

        [Test]
        public async Task RegisterStudentAsync_Success_ReturnsSuccessMessage()
        {
            // Arrange
            var dto = CreateValidStudentDto();

            IdentityResult identityResult = IdentityResult.Success;

            _mockStudentRepository.Setup(r => r.IsUsernameTakenAsync(dto.Username)).ReturnsAsync(false);
            _mockStudentRepository.Setup(r => r.IsEmailTakenAsync(dto.Email)).ReturnsAsync(false);
            _mockStudentRepository.Setup(r => r.CreateUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(identityResult);

            _mockStudentRepository.Setup(r => r.AddUserToRoleAsync(It.IsAny<ApplicationUser>(), "Student"))
                .Returns(Task.FromResult(identityResult));

            _mockStudentRepository.Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.RegisterStudentAsync(dto);

            // Assert
            Assert.AreEqual("Student Registration Success", result);
        }

        private RegisterStudentDto CreateValidStudentDto()
        {
            return new RegisterStudentDto
            {
                FirstName = "John",
                LastName = "Doe",
                Username = "johndoe",
                Email = "john.doe@example.com",
                Phone = "+201234567890",
                Level = "Beginner",
                Password = "password123"
            };
        }

    }
}
