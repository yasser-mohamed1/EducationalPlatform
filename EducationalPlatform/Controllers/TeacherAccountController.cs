using EducationalPlatform.Data;
using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherAccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly EduPlatformContext context;

        public TeacherAccountController(UserManager<ApplicationUser> userManager, EduPlatformContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registeration(RegisterTeacherDto teacherDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = teacherDto.Username;
                user.Email = teacherDto.Email;
                user.PhoneNumber = teacherDto.Phone;

                IdentityResult result = await userManager.CreateAsync(user, teacherDto.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Teacher");
                    user.Teacher = new();
                    user.Teacher.FirstName = teacherDto.FirstName;
                    user.Teacher.LastName = teacherDto.LastName;
                    user.Teacher.Address = teacherDto.Address;
                    user.Teacher.City = teacherDto.City;
                    user.Teacher.Governorate = teacherDto.Governorate;
                    user.Teacher.gender = teacherDto.gender;
                    await context.Teachers.AddAsync(user.Teacher);
                    await context.SaveChangesAsync();
                    return Ok("Teacher Registeration Success");
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);
        }
    }
}
