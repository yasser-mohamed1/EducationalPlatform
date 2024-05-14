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
        private readonly EduPlatformContext context;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<ApplicationUser> userManager;

        public TeacherAccountController(UserManager<ApplicationUser> userManager, EduPlatformContext context, IWebHostEnvironment env)
        {
            _env = env;
            this.userManager = userManager;
            this.context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registeration([FromForm] RegisterTeacherDto teacherDto)
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
                    user.Teacher.Governorate = teacherDto.Governorate;
                    if (teacherDto.ProfileImage != null && teacherDto.ProfileImage.Length > 0)
                    {
                        user.Teacher.ProfileImageUrl = UploadProfileImage(teacherDto.ProfileImage);
                    }
                    await context.Teachers.AddAsync(user.Teacher);
                    await context.SaveChangesAsync();
                    return Ok("Teacher Registeration Success");
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);
        }

        private string UploadProfileImage(IFormFile ImageFile)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                ImageFile.CopyTo(stream);
            }

            return ("http://edu1.runasp.net/uploads/" + fileName);
        }

    }
}
