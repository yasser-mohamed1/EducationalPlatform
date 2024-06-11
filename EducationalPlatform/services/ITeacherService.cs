using EducationalPlatform.DTO;

public interface ITeacherService
{
    Task<List<TeacherDto>> GetTeachersAsync();
    Task<TeacherDto> GetTeacherByIdAsync(int id);
    Task<bool> UpdateTeacherAsync(int id, UpdateTeacherDto dto, HttpContext httpContext);
    Task<bool> DeleteTeacherAsync(int id);
}