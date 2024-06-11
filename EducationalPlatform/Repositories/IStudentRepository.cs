using EducationalPlatform.DTO;
using EducationalPlatform.Entities;

namespace EducationalPlatform.Repositories
{
    public interface IStudentRepository
    {
        Task<List<StudentDTO>> GetStudentsAsync();
        Task<StudentDTO> GetStudentByIdAsync(int id);
        Task<Student> GetStudentUserByIdAsync(int id);
        Task<bool> UpdateStudentAsync(Student student);
        Task<bool> DeleteStudentAsync(int id);
        Task<IEnumerable<SearchSubjectDto>> SearchSubjects(string searchQuery, string governorate);
        Task<IEnumerable<TeacherWithSubjectDTO>> SearchTeachers(string searchQuery, string governorate);
    }
}
