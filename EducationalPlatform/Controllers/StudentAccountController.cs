using EducationalPlatform.DTO;
using EducationalPlatform.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAccountController : ControllerBase
    {
        private readonly IStudentAccountService _studentService;
        //private readonly IWebHostEnvironment _env;

        public StudentAccountController(IStudentAccountService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration([FromForm] RegisterStudentDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _studentService.RegisterStudentAsync(studentDto);
            if (result != "Student Registration Success")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
