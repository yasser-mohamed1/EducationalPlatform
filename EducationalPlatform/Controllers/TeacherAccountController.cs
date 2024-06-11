using EducationalPlatform.DTO;
using EducationalPlatform.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherAccountController : ControllerBase
    {
        private readonly ITeacherAccountService _teacherService;
        //private readonly IWebHostEnvironment _env;

        public TeacherAccountController(ITeacherAccountService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration([FromForm] RegisterTeacherDto teacherDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _teacherService.RegisterTeacherAsync(teacherDto);
            if (result != "Teacher Registration Success")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
