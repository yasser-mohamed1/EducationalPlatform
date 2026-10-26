using EducationalPlatform.Dtos;

namespace EducationalPlatform.Services
{
    public interface ITeacherAccountService
    {
        Task<string> RegisterTeacherAsync(RegisterTeacherDto teacherDto);
    }
}
