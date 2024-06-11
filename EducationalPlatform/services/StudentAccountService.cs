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

            var result = await _studentRepository.CreateUserAsync(user, studentDto.Password);

            if (!result.Succeeded)
            {
                return string.Join(", ", result.Errors.Select(e => e.Description));
            }

            await _studentRepository.AddUserToRoleAsync(user, "Student");
            //await _studentRepository.AddStudentAsync(user.Student);
            await _studentRepository.SaveChangesAsync();

            return "Student Registration Success";
        }

       
    }
}
