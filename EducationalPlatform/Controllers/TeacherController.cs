using EducationalPlatform.DTO;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;

    public TeacherController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet]
    public async Task<ActionResult> GetTeachers()
    {
        var teachers = await _teacherService.GetTeachersAsync();
        return Ok(teachers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetTeacher(int id)
    {
        var teacher = await _teacherService.GetTeacherByIdAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }

        return Ok(teacher);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeacher(int id, [FromForm] UpdateTeacherDto dto)
    {
        var result = await _teacherService.UpdateTeacherAsync(id, dto, HttpContext);
        if (!result)
        {
            return NotFound($"No Teacher was found with id : {id}");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeacher(int id)
    {
        var result = await _teacherService.DeleteTeacherAsync(id);
        if (!result)
        {
            return NotFound($"No Teacher was found with id : {id}");
        }

        return NoContent();
    }
}
