using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using EducationalPlatform.Repositories;
using Microsoft.AspNetCore.Identity;

namespace EducationalPlatform.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IWebHostEnvironment _env;
        private readonly Func<HttpContext, UserManager<ApplicationUser>> _userManagerFactory;

        public StudentService(IStudentRepository studentRepository, IWebHostEnvironment env, Func<HttpContext, UserManager<ApplicationUser>> userManagerFactory)
        {
            _studentRepository = studentRepository;
            _env = env;
            _userManagerFactory = userManagerFactory;
        }

        public Task<List<StudentDTO>> GetStudentsAsync()
        {
            return _studentRepository.GetStudentsAsync();
        }

        public Task<StudentDTO> GetStudentByIdAsync(int id)
        {
            return _studentRepository.GetStudentByIdAsync(id);
        }

        public async Task<bool> UpdateStudentAsync(int id, UpdateStudentDTO dto, HttpContext httpContext)
        {
            var existingStudent = await _studentRepository.GetStudentUserByIdAsync(id);
            if (existingStudent == null)
                return false;

            if (!string.IsNullOrEmpty(dto.FirstName))
                existingStudent.FirstName = dto.FirstName;

            if (!string.IsNullOrEmpty(dto.LastName))
                existingStudent.LastName = dto.LastName;

            if (!string.IsNullOrEmpty(dto.Phone))
                existingStudent.User.PhoneNumber = dto.Phone;

            if (dto.ProfileImage != null && dto.ProfileImage.Length > 0)
            {
                var profileImageUrl = await UploadProfileImageAsync(id, dto.ProfileImage);
                existingStudent.ProfileImageUrl = profileImageUrl;
            }

            if (!string.IsNullOrEmpty(dto.Level))
            {
                existingStudent.Level = dto.Level;
            }

            if (!string.IsNullOrEmpty(dto.Password))
            {
                var userManager = _userManagerFactory.Invoke(httpContext);
                var newPasswordHash = userManager.PasswordHasher.HashPassword(existingStudent.User, dto.Password);
                existingStudent.User.PasswordHash = newPasswordHash;
            }
            try
            {
                await _studentRepository.UpdateStudentAsync(existingStudent);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> DeleteStudentAsync(int id)
        {
            return _studentRepository.DeleteStudentAsync(id);
        }

        public async Task<IEnumerable<SearchSubjectDto>> SearchSubjects(string searchQuery, string governorate)
        {
            return await _studentRepository.SearchSubjects(searchQuery, governorate);
        }

        public async Task<IEnumerable<TeacherWithSubjectDTO>> SearchTeachers(string searchQuery, string governorate)
        {
            return await _studentRepository.SearchTeachers(searchQuery, governorate);
        }

        private async Task<string> UploadProfileImageAsync(int id, IFormFile imageFile)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
                return "";

            if (!string.IsNullOrEmpty(student.ProfileImageUrl))
            {
                var oldImagePath = Path.Combine(_env.WebRootPath, student.ProfileImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return "http://edu1.runasp.net/uploads/" + fileName;
        }
    }

}
