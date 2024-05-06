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
        private readonly EduPlatformContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<ApplicationUser> userManager;

        public StudentAccountController(UserManager<ApplicationUser> userManager, EduPlatformContext context, IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
            this.userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registeration([FromForm] RegisterStudentDto studentDto)
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
                    if (studentDto.ProfileImage != null && studentDto.ProfileImage.Length > 0)
                    {
                        user.Student.ProfileImageUrl = UploadProfileImage(studentDto.ProfileImage);
                    }
                    await _context.Students.AddAsync(user.Student);
                    await _context.SaveChangesAsync();
                    return Ok("Student Registeration Success");
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
