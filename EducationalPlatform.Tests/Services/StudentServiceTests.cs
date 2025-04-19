using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using EducationalPlatform.Repositories;
using EducationalPlatform.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace EducationalPlatform.Tests.Services
{
    [TestFixture]
    public class StudentServiceTests
    {
        private Mock<IStudentRepository> _mockRepo;
        private StudentService _service;
        private Mock<IWebHostEnvironment> _mockEnv;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IStudentRepository>();
            _mockEnv = new Mock<IWebHostEnvironment>();
            _service = new StudentService(
                _mockRepo.Object,
                _mockEnv.Object,
                hc => Mock.Of<UserManager<ApplicationUser>>()
            );
        }

        [Test]
        public async Task SearchSubjects_ValidQuery_ReturnsResults()
        {
            // Arrange
            var expectedResults = new List<SearchSubjectDto>
            {
                new SearchSubjectDto { subjectId = 1, subjName = "Advanced Mathematics" }
            };

            _mockRepo.Setup(r => r.SearchSubjects("math", null))
                    .ReturnsAsync(expectedResults);

            // Act
            var results = await _service.SearchSubjects("math", null);

            // Assert
            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("Advanced Mathematics", results.First().subjName);
        }

        [Test]
        public async Task SearchTeachers_GovernorateFilter_ReturnsFiltered()
        {
            // Arrange
            var mockTeachers = new List<TeacherWithSubjectDTO>
        {
            new TeacherWithSubjectDTO { Governorate = "Cairo" },
            new TeacherWithSubjectDTO { Governorate = "Alexandria" }
        };

            _mockRepo.Setup(r => r.SearchTeachers(null, "Cairo"))
                    .ReturnsAsync(mockTeachers.Where(t => t.Governorate == "Cairo"));

            // Act
            var results = await _service.SearchTeachers(null, "Cairo");

            // Assert
            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("Cairo", results.First().Governorate);
        }

        [Test]
        public async Task SearchSubjects_EmptyQuery_ReturnsAllInGovernorate()
        {
            // Arrange
            var mockSubjects = new List<SearchSubjectDto>
        {
            new SearchSubjectDto { subjectId = 1 },
            new SearchSubjectDto { subjectId = 2 }
        };

            _mockRepo.Setup(r => r.SearchSubjects("", "Giza"))
                    .ReturnsAsync(mockSubjects);

            // Act
            var results = await _service.SearchSubjects("", "Giza");

            // Assert
            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public async Task SearchTeachers_CombinedFilters_ReturnsAccurateResults()
        {
            // Arrange
            var mockTeachers = new List<TeacherWithSubjectDTO>
        {
            new TeacherWithSubjectDTO { FirstName = "Ali", Governorate = "Cairo" },
            new TeacherWithSubjectDTO { FirstName = "Ali", Governorate = "Alexandria" }
        };

            _mockRepo.Setup(r => r.SearchTeachers("Ali", "Cairo"))
                    .ReturnsAsync(mockTeachers.Where(t =>
                        t.FirstName.Contains("Ali") &&
                        t.Governorate == "Cairo"));

            // Act
            var results = await _service.SearchTeachers("Ali", "Cairo");

            // Assert
            Assert.AreEqual(1, results.Count());
            Assert.Multiple(() =>
            {
                Assert.That(results.First().FirstName, Does.Contain("Ali"));
                Assert.That(results.First().Governorate, Is.EqualTo("Cairo"));
            });
        }
    }
}