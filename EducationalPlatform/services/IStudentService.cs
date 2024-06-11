using EducationalPlatform.DTO;

namespace EducationalPlatform.Services
{
    public interface IStudentService
    {
        Task<List<StudentDTO>> GetStudentsAsync();
        Task<StudentDTO> GetStudentByIdAsync(int id);
        Task<bool> UpdateStudentAsync(int id, UpdateStudentDTO dto, HttpContext httpContext);
        Task<bool> DeleteStudentAsync(int id);
        Task<IEnumerable<SearchSubjectDto>> SearchSubjects(string searchQuery, string governorate);
        Task<IEnumerable<TeacherWithSubjectDTO>> SearchTeachers(string searchQuery, string governorate);
    }
}