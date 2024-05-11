using EducationalPlatform.Data;
using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static EducationalPlatform.DTO.TeacherDto;

namespace EducationalPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly EduPlatformContext _context;
        private readonly Func<HttpContext, UserManager<ApplicationUser>> _userManagerFactory;

        public StudentController(EduPlatformContext context, Func<HttpContext, UserManager<ApplicationUser>> userManagerFactory, IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
            _userManagerFactory = userManagerFactory;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult> GetStudents()
        {
            var students = await _context.Students
                .Include(s => s.User)
                .Select(s => new StudentDTO
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.User.Email,
                    Phone = s.User.PhoneNumber,
                    ProfileImageUrl = s.ProfileImageUrl,
                    Level = s.Level
                })
                .ToListAsync();

            return Ok(students);
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetStudent(int id)
        {
            var student = await _context.Students
                .Include(s => s.User)
                .Where(s => s.Id == id)
                .Select(s => new StudentDTO
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.User.Email,
                    Phone = s.User.PhoneNumber,
                    ProfileImageUrl = s.ProfileImageUrl,
                    Level = s.Level
                })
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, [FromForm] UpdateStudentDTO dto)
        {
            if (!StudentExists(id))
            {
                return NotFound($"No Student was found with id : {id}");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingStudent = await _context.Students.Include(s => s.User).FirstOrDefaultAsync(s => s.Id == id);

            if (existingStudent == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(dto.FirstName))
            {
                existingStudent.FirstName = dto.FirstName;
            }
            if(!string.IsNullOrEmpty(dto.LastName))
            {
                existingStudent.LastName = dto.LastName;
            }
            if (!string.IsNullOrEmpty(dto.Phone))
            {
                existingStudent.User.PhoneNumber = dto.Phone;
            }
            if (dto.ProfileImage != null && dto.ProfileImage.Length > 0)
            {
                UploadProfileImage(id, dto.ProfileImage);
            }
            if (!string.IsNullOrEmpty(dto.Level))
            {
                existingStudent.Level = dto.Level;
            }

            if (!string.IsNullOrEmpty(dto.Password))
            {
                var userManager = _userManagerFactory.Invoke(HttpContext);
                var newPasswordHash = userManager.PasswordHasher.HashPassword(existingStudent.User, dto.Password);
                existingStudent.User.PasswordHash = newPasswordHash;
            }

            try
            {
                _context.Entry(existingStudent).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound($"No Student was found with id : {id}");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.userId == id);
            if (user == null)
            {
                return NotFound($"No Student was found with id : {id}");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        private string UploadProfileImage(int id, IFormFile ImageFile)
        {
            var student =  _context.Students.Find(id);

            if (student == null)
            {
                return "";
            }

            // Check if the student already has a profile image
            if (!string.IsNullOrEmpty(student.ProfileImageUrl))
            {
                // Delete the old profile image from the server
                var oldImagePath = Path.Combine(_env.WebRootPath, student.ProfileImageUrl.TrimStart('/'));
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

            student.ProfileImageUrl = "http://edu1.runasp.net/uploads/" + fileName; // Update the profile image URL
             _context.SaveChanges();

            return filePath;
        }

        [HttpGet("search")]
        public IActionResult SearchTeachersAndSubjects(string teacherName = null, string subjectName = null)
        {
            var query = _context.Teachers
                .Include(t => t.Subjects)
                .Select(t => new TeacherWithSubjectDTO
                {
                    Id = t.Id,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    ProfileImageUrl = t.ProfileImageUrl,
                    Address = t.Address,
                    Email = t.User.Email,
                    Phone = t.User.PhoneNumber,
                    Subjects = t.Subjects.Select(s => s.subjName).ToList()
                })
                .AsQueryable();

            if (!string.IsNullOrEmpty(teacherName))
            {
                query = query.Where(t => t.FirstName.Contains(teacherName) || t.LastName.Contains(teacherName));
            }

            if (!string.IsNullOrEmpty(subjectName))
            {
                query = query.Where(t => t.Subjects.Any(s => s.Contains(subjectName)));
            }

            var result = query.ToList();

            return Ok(result);
        }




    }
}
