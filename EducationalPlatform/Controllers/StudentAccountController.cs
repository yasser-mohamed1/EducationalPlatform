using EducationalPlatform.Data;
using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAccountController : ControllerBase
    {
        private readonly EduPlatformContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public StudentAccountController(UserManager<ApplicationUser> userManager, EduPlatformContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registeration(RegisterStudentDto studentDto)
        {
            if(ModelState.IsValid) 
            {
                ApplicationUser user = new();
                user.UserName = studentDto.Username;
                user.Email = studentDto.Email;
                user.PhoneNumber = studentDto.Phone;
                IdentityResult result = await userManager.CreateAsync(user, studentDto.Password);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Student");
                    user.Student = new();
                    user.Student.FirstName = studentDto.FirstName;
                    user.Student.LastName = studentDto.LastName;
                    user.Student.Level = studentDto.Level;
                    user.Student.ProfileImageUrl = studentDto.ProfileImageUrl;
                    await context.Students.AddAsync(user.Student);
                    await context.SaveChangesAsync();
                    return Ok("Student Registeration Success");
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);
        }

    }
}
