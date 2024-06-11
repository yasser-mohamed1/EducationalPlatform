using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPlatform.Repositories
{
    public interface ITeacherRepository
    {
        Task<List<TeacherDto>> GetTeachersAsync();
        Task<TeacherDto> GetTeacherByIdAsync(int id);
        Task<Teacher> GetTeacherUserByIdAsync(int id);
        Task UpdateTeacherAsync(Teacher teacher);
        Task<bool> DeleteTeacherAsync(int id);
    }
}
