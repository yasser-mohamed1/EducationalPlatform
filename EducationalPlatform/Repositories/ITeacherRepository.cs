using EducationalPlatform.DTO;

namespace EducationalPlatform.Repositories
{
    public interface ITeacherRepository
    {
        Task<List<TeacherDto>> GetTeachersAsync();
        Task<TeacherDto> GetTeacherByIdAsync(int id);
        Task<bool> UpdateTeacherAsync(int id, UpdateTeacherDto dto);
        Task<bool> DeleteTeacherAsync(int id);
    }
}
