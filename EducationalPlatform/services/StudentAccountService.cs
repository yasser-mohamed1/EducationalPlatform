using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using EducationalPlatform.Repositories;

namespace EducationalPlatform.Services
{
    public class StudentAccountService : IStudentAccountService
    {
        private readonly IStudentAccountRepository _studentRepository;

        public StudentAccountService(IStudentAccountRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<string> RegisterStudentAsync(RegisterStudentDto studentDto)
        {
            // Check if username or email already exists (this could be done by calling the repository)
            if (await _studentRepository.IsUsernameTakenAsync(studentDto.Username))
            {
                return "Username already exists";
            }

            if (await _studentRepository.IsEmailTakenAsync(studentDto.Email))
            {
                return "Email is already registered";
            }

            var user = new ApplicationUser
            {
                UserName = studentDto.Username,
                Email = studentDto.Email,
                PhoneNumber = studentDto.Phone,
                Student = new Student
                {
                    FirstName = studentDto.FirstName,
                    LastName = studentDto.LastName,
                    Level = studentDto.Level,
                }
            };

            // Create user in the repository
            var result = await _studentRepository.CreateUserAsync(user, studentDto.Password);

            if (!result.Succeeded)
            {
                return string.Join(", ", result.Errors.Select(e => e.Description));
            }

            await _studentRepository.AddUserToRoleAsync(user, "Student");
            await _studentRepository.SaveChangesAsync();

            return "Student Registration Success";
        }
    }
}
