using EducationalPlatform.DTO;

namespace EducationalPlatform.Services
{
    public interface IStudentAccountService
    {
        Task<string> RegisterStudentAsync(RegisterStudentDto studentDto);
    }
}
