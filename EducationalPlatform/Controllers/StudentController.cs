using EducationalPlatform.DTO;
using EducationalPlatform.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<ActionResult> GetStudents()
    {
        var students = await _studentService.GetStudentsAsync();
        return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetStudent(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutStudent(int id, [FromForm] UpdateStudentDTO dto)
    {
        var result = await _studentService.UpdateStudentAsync(id, dto, HttpContext);
        if (!result)
        {
            return NotFound($"No Student was found with id : {id}");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var result = await _studentService.DeleteStudentAsync(id);
        if (!result)
        {
            return NotFound($"No Student was found with id : {id}");
        }

        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchTeachers(string searchQuery = null, string governorate = null)
    {
        var subjects = await _studentService.SearchSubjects(searchQuery, governorate);

        if (subjects.Any())
        {
            return Ok(subjects);
        }

        var teachers = await _studentService.SearchTeachers(searchQuery, governorate);

        if (teachers.Any())
        {
            return Ok(teachers);
        }

        return NotFound("No results found.");
    }
}
