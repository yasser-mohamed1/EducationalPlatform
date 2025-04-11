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
            Assert.That(result, Is.EqualTo("Username already exists"));
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
            Assert.That(result, Is.EqualTo("Email is already registered"));
        }

        [Test]
        public async Task RegisterStudentAsync_FailedUserCreation_ReturnsErrorMessage()
        {
            // Arrange
            var dto = CreateValidStudentDto();
            dto.Password = "123";

            // Act
            var result = await _service.RegisterStudentAsync(dto);

            // Assert
            Assert.That(result, Is.EqualTo("Weak password"));
        }

        [Test]
        public async Task RegisterStudentAsync_Success_ReturnsSuccessMessage()
        {
            // Arrange
            var dto = CreateValidStudentDto();
            var identityResult = IdentityResult.Success;

            _mockStudentRepository.Setup(r => r.IsUsernameTakenAsync(dto.Username)).ReturnsAsync(false);
            _mockStudentRepository.Setup(r => r.IsEmailTakenAsync(dto.Email)).ReturnsAsync(false);
            _mockStudentRepository.Setup(r => r.CreateUserAsync(It.IsAny<ApplicationUser>(), dto.Password))
                .ReturnsAsync(identityResult);
            _mockStudentRepository.Setup(r => r.AddUserToRoleAsync(It.IsAny<ApplicationUser>(), "Student"))
                .ReturnsAsync(identityResult);
            _mockStudentRepository.Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.RegisterStudentAsync(dto);

            // Assert
            Assert.That(result, Is.EqualTo("Student Registration Success"));
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
                Password = "password123A!"
            };
        }
    }
}