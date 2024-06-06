﻿using EducationalPlatform.Data;
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
                    userName = s.User.UserName,
                    Email = s.User.Email,
                    Phone = s.User.PhoneNumber,
                    ProfileImageUrl = s.ProfileImageUrl,
                    Level = s.Level
                })
                .ToListAsync();

            return Ok(students);
        }

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
                    userName = s.User.UserName,
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

            if (!string.IsNullOrEmpty(student.ProfileImageUrl))
            {
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

            student.ProfileImageUrl = "http://edu1.runasp.net/uploads/" + fileName;
             _context.SaveChanges();

            return filePath;
        }

        [HttpGet("search")]
        public IActionResult SearchTeachers(string searchQuery = null, string Governorate = null)
        {
            if (!string.IsNullOrEmpty(searchQuery) && string.IsNullOrEmpty(Governorate))
            {
                var querys = _context.Subjects.Include(s => s.Teacher)
                    .AsEnumerable()
                    .Where(s => (s.subjName.LevenshteinDistance(searchQuery) <= 3) && ContainsMajorityOfCharacters(s.subjName, searchQuery))
                    .Select(s => new SearchSubjectDto()
                    {
                        subjectId = s.Id,
                        subjName = s.subjName,
                        Level = s.Level,
                        Describtion = s.Describtion,
                        pricePerHour = s.pricePerHour,
                        Term = s.Term,
                        AddingTime = s.AddingTime,
                        TeacherId = s.TeacherId,
                        teacherName = s.Teacher.FirstName + " " + s.Teacher.LastName,
                        profileImageUrl = s.Teacher.ProfileImageUrl
                    }).AsQueryable();
                var results = querys.ToList();

                if (results.Count > 0)
                {
                    return Ok(results);
                }
            }

            if (!string.IsNullOrEmpty(searchQuery) && !string.IsNullOrEmpty(Governorate))
            {
                var querys = _context.Subjects.Include(s => s.Teacher)
                    .AsEnumerable()
                    .Where(s => (s.subjName.LevenshteinDistance(searchQuery) <= 3) && ContainsMajorityOfCharacters(s.subjName, searchQuery)
                    && s.Teacher.Governorate == Governorate)
                    .Select(s => new SearchSubjectDto()
                    {
                        subjectId = s.Id,
                        subjName = s.subjName,
                        Level = s.Level,
                        Describtion = s.Describtion,
                        pricePerHour = s.pricePerHour,
                        Term = s.Term,
                        AddingTime = s.AddingTime,
                        TeacherId = s.TeacherId,
                        teacherName = s.Teacher.FirstName + " " + s.Teacher.LastName,
                        profileImageUrl = s.Teacher.ProfileImageUrl
                    }).AsQueryable();
                var results = querys.ToList();

                if (results.Count > 0)
                {
                    return Ok(results);
                }
            }

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
                    Governorate = t.Governorate,
                    Subjects = t.Subjects.Select(s => new SubjectDto()
                    {
                        subjectId = s.Id,
                        subjName = s.subjName,
                        Level = s.Level,
                        Describtion = s.Describtion,
                        pricePerHour = s.pricePerHour,
                        Term = s.Term,
                        AddingTime = s.AddingTime,
                    })
                    .ToList()
                })
                .AsEnumerable();

            if (!string.IsNullOrEmpty(Governorate) && !string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(t => ((t.FirstName + t.LastName).LevenshteinDistance(searchQuery) <= 3 ||
                t.FirstName.LevenshteinDistance(searchQuery) <= 1 ||
                t.LastName.LevenshteinDistance(searchQuery) <= 1) &&
                t.Governorate == Governorate);
            }

            if (!string.IsNullOrEmpty(Governorate) && string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(t => t.Governorate == Governorate);
            }

            if (string.IsNullOrEmpty(Governorate) && !string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(t => (t.FirstName + t.LastName).LevenshteinDistance(searchQuery) <= 3 ||
                t.FirstName.LevenshteinDistance(searchQuery) <= 1 ||
                t.LastName.LevenshteinDistance(searchQuery) <= 1);
            }

            var result = query.ToList();

            return Ok(result);
        }

        static bool ContainsMajorityOfCharacters(string source, string target)
        {
            var sourceChars = source.GroupBy(c => c)
                                    .ToDictionary(g => g.Key, g => g.Count());

            var targetChars = target.GroupBy(c => c)
                                    .ToDictionary(g => g.Key, g => g.Count());

            int matchingCharactersCount = 0;
            int requiredMajorityCount = (int)Math.Ceiling(targetChars.Sum(kv => kv.Value) / 2.0);

            foreach (var targetChar in targetChars)
            {
                if (sourceChars.ContainsKey(targetChar.Key))
                {
                    matchingCharactersCount += Math.Min(sourceChars[targetChar.Key], targetChar.Value);
                }
            }

            return matchingCharactersCount >= requiredMajorityCount;
        }
    }

    public static class StringExtensions
    {
        public static int LevenshteinDistance(this string a, string b)
        {
            if (string.IsNullOrEmpty(a)) return b.Length;
            if (string.IsNullOrEmpty(b)) return a.Length;

            int[,] costs = new int[a.Length + 1, b.Length + 1];

            for (int i = 0; i <= a.Length; i++)
                costs[i, 0] = i;

            for (int j = 0; j <= b.Length; j++)
                costs[0, j] = j;

            for (int i = 1; i <= a.Length; i++)
            {
                for (int j = 1; j <= b.Length; j++)
                {
                    int cost = (b[j - 1] == a[i - 1]) ? 0 : 1;
                    costs[i, j] = Math.Min(Math.Min(costs[i - 1, j] + 1, costs[i, j - 1] + 1), costs[i - 1, j - 1] + cost);
                }
            }

            return costs[a.Length, b.Length];
        }
    }
}
