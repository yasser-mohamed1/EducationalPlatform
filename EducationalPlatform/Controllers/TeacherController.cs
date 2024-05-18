using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EducationalPlatform.Data;
using EducationalPlatform.Entities;
using Microsoft.EntityFrameworkCore;
using EducationalPlatform.DTO;
using static EducationalPlatform.DTO.TeacherDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EducationalPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly EduPlatformContext _context;
        private readonly Func<HttpContext, UserManager<ApplicationUser>> _userManagerFactory;

        public TeacherController(EduPlatformContext context, Func<HttpContext,
            UserManager<ApplicationUser>> userManagerFactory, IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
            _userManagerFactory = userManagerFactory;
        }

        // GET: api/Teacher
        [HttpGet]
        public async Task<ActionResult> GetTeachers()
        {
            var teachers = await _context.Teachers
                .Include(t => t.User)
                .Select(t => new TeacherDTO
                {
                    Id = t.Id,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Governorate = t.Governorate,
                    Address = t.Address,
                    Email = t.User.Email,
                    Phone = t.User.PhoneNumber,
                    ProfileImageUrl = t.ProfileImageUrl
                })
                .ToListAsync();

            return Ok(teachers);
        }

        // GET: api/Teacher/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTeacher(int id)
        {
            var teacher = await _context.Teachers
                .Include(t => t.User)
                .Where(t => t.Id == id)
                .Select(t => new TeacherDTO
                {
                    Id = t.Id,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Governorate = t.Governorate,
                    Address = t.Address,
                    Email = t.User.Email,
                    Phone = t.User.PhoneNumber,
                    ProfileImageUrl = t.ProfileImageUrl
                })
                .FirstOrDefaultAsync();

            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTeacher(int id, [FromForm] UpdateTeacherDto dto)
        {
            if (!TeacherExists(id))
            {
                return NotFound($"No Teacher was found with id : {id}");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTeacher = _context.Teachers.Include(t => t.User).FirstOrDefault(t => t.Id == id);

            if (existingTeacher == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(dto.FirstName))
            {
                existingTeacher.FirstName = dto.FirstName;
            }
            if (!string.IsNullOrEmpty(dto.LastName))
            {
                existingTeacher.LastName = dto.LastName;
            }
            if (!string.IsNullOrEmpty(dto.Phone))
            {
                existingTeacher.User.PhoneNumber = dto.Phone;
            }
            if (dto.ProfileImage != null && dto.ProfileImage.Length > 0)
            {
                UploadProfileImage(id, dto.ProfileImage);
            }
            if (!string.IsNullOrEmpty(dto.Address))
            {
                existingTeacher.Address = dto.Address;
            }

            if (!string.IsNullOrEmpty(dto.Password))
            {
                var userManager = _userManagerFactory.Invoke(HttpContext);
                var newPasswordHash = userManager.PasswordHasher.HashPassword(existingTeacher.User, dto.Password);
                existingTeacher.User.PasswordHash = newPasswordHash;
            }

            try
            {
                _context.Entry(existingTeacher).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
                {
                    return NotFound($"No Teacher was found with id : {id}");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeacher(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.userId == id);
            if (user == null)
            {
                return NotFound($"No Teacher was found with id : {id}");
            }

            _context.Users.Remove(user);

            _context.SaveChanges();

            return NoContent();
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }

        private string UploadProfileImage(int id, IFormFile ImageFile)
        {
            var teacher = _context.Teachers.Find(id);

            if (teacher == null)
            {
                return "";
            }

            // Check if the student already has a profile image
            if (!string.IsNullOrEmpty(teacher.ProfileImageUrl))
            {
                // Delete the old profile image from the server
                var oldImagePath = Path.Combine(_env.WebRootPath, teacher.ProfileImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

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

            teacher.ProfileImageUrl = "http://edu1.runasp.net/uploads/" + fileName; // Update the profile image URL
            _context.SaveChanges();

            return filePath;
        }

        // GET: api/teacher/{teacherId}/subjects
        //[HttpGet("{teacherId}/subjects")]
        //public async Task<ActionResult<IEnumerable<SubjectDto>>> GetTeacherSubjects(int teacherId)
        //{
        //    var teacher = await _context.Teachers
        //        .Include(t => t.Subjects)
        //        .FirstOrDefaultAsync(t => t.Id == teacherId);

        //    if (teacher == null)
        //    {
        //        return NotFound("Teacher not found");
        //    }

        //    var subjects = teacher.Subjects.Select(s => new SubjectDto
        //    {
        //        Id = s.Id,
        //        subjName = s.subjName,
        //        Level = s.Level,
        //        Describtion = s.Describtion,
        //        pricePerHour = s.pricePerHour,
        //        AddingTime = s.AddingTime
        //    });

        //    return Ok(subjects);
        //}

    } 
}
