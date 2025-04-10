using EducationalPlatform.Controllers;
using EducationalPlatform.DTO;
using EducationalPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EducationalPlatform.Tests.Controllers
{
    [TestFixture]
    public class StudentAccountControllerTests
    {
        private Mock<IStudentAccountService> _mockStudentService;
        private StudentAccountController _controller;

        [SetUp]
        public void Setup()
        {
            _mockStudentService = new Mock<IStudentAccountService>();
            _controller = new StudentAccountController(_mockStudentService.Object);
        }

        private RegisterStudentDto CreateValidStudentDto()
        {
            return new RegisterStudentDto
            {
                FirstName = "Yasser",
                LastName = "Ali",
                Username = "yasser.ali",
                Email = "yasser@example.com",
                Phone = "+201234567890",
                Level = "4th Year",
                Password = "SecurePassword123"
            };
        }

        [Test]
        public async Task Registration_ModelStateInvalid_ReturnsBadRequest()
        {
            // Arrange
            var invalidDto = CreateValidStudentDto();
            invalidDto.Email = ""; // Invalid email
            _controller.ModelState.AddModelError("Email", "Required");

            // Act
            var result = await _controller.Registration(invalidDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task Registration_Successful_ReturnsOk()
        {
            // Arrange
            var dto = CreateValidStudentDto();
            _mockStudentService.Setup(s => s.RegisterStudentAsync(dto))
                .ReturnsAsync("Student Registration Success");

            // Act
            var result = await _controller.Registration(dto) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("Student Registration Success", result.Value);
        }

        [Test]
        public async Task Registration_Failure_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var dto = CreateValidStudentDto();
            _mockStudentService.Setup(s => s.RegisterStudentAsync(dto))
                .ReturnsAsync("Username already exists");

            // Act
            var result = await _controller.Registration(dto) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Username already exists", result.Value);
        }

        [Test]
        public async Task Registration_InvalidPhoneFormat_ReturnsBadRequest()
        {
            // Arrange
            var dto = CreateValidStudentDto();
            dto.Phone = "012345"; // Invalid phone (does not match +20 followed by 10 digits)

            _controller.ModelState.AddModelError("Phone", "The phone number is not in the correct format.");

            // Act
            var result = await _controller.Registration(dto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task Registration_MissingUsername_ReturnsBadRequest()
        {
            var dto = CreateValidStudentDto();
            dto.Username = "";
            _controller.ModelState.AddModelError("Username", "Required");

            var result = await _controller.Registration(dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task Registration_TooLongFirstName_ReturnsBadRequest()
        {
            var dto = CreateValidStudentDto();
            dto.FirstName = new string('A', 51); // Exceeds max length

            _controller.ModelState.AddModelError("FirstName", "String length exceeds limit");


            var result = await _controller.Registration(dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}