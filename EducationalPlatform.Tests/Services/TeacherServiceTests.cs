using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using EducationalPlatform.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace EducationalPlatform.Tests.Services
{
    [TestFixture]
    public class TeacherServiceTests
    {
        private Mock<ITeacherRepository> _mockRepo;
        private Mock<IWebHostEnvironment> _mockEnv;
        private Mock<UserManager<ApplicationUser>> _mockUserManager;
        private TeacherService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<ITeacherRepository>();
            _mockEnv = new Mock<IWebHostEnvironment>();
            _mockEnv.Setup(e => e.WebRootPath).Returns("/test/path");

            var store = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                store.Object,
                null,
                new PasswordHasher<ApplicationUser>(),
                new IUserValidator<ApplicationUser>[0],
                new IPasswordValidator<ApplicationUser>[0],
                null,
                null,
                null,
                null
            );

            Func<HttpContext, UserManager<ApplicationUser>> userManagerFactory = ctx => _mockUserManager.Object;

            _service = new TeacherService(userManagerFactory, _mockRepo.Object, _mockEnv.Object);
        }

        [Test]
        public async Task UpdateTeacherAsync_ChangePassword_UpdatesHash()
        {
            // Arrange
            var teacher = new Teacher { User = new ApplicationUser() };
            var dto = new UpdateTeacherDto { Password = "NewPass123!" };
            _mockRepo.Setup(r => r.GetTeacherUserByIdAsync(1)).ReturnsAsync(teacher);

            // Act
            var result = await _service.UpdateTeacherAsync(1, dto, new DefaultHttpContext());

            // Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(teacher.User.PasswordHash);
        }

        [Test]
        public async Task UploadProfileImageAsync_ValidFile_ReturnsUrl()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns("test.jpg");
            fileMock.Setup(f => f.Length).Returns(1024);
            fileMock.Setup(f => f.ContentType).Returns("image/jpeg");

            _mockRepo.Setup(r => r.GetTeacherByIdAsync(1))
                    .ReturnsAsync(new TeacherDto());

            // Act
            var result = await _service.UploadProfileImageAsync(1, fileMock.Object);

            // Assert
            Assert.That(result, Does.StartWith("http://edu1.runasp.net/uploads/"));
        }
    }
}