using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EducationalPlatform.Data;
using EducationalPlatform.Entities;
using Microsoft.EntityFrameworkCore;
using EducationalPlatform.DTO;

namespace EducationalPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly EduPlatformContext context;

        public TeacherController(EduPlatformContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllTeachers()
        {
            List<Teacher> teachers = context.Teachers.ToList();

            List<TeacherDto> dtos = [];

            foreach(var teacher in teachers)
            {
                TeacherDto dto = new ();
                dto.Id = teacher.Id;
                dto.Address = teacher.Address;
                teacher.User = context.Users.FirstOrDefault(u => u.userId == teacher.Id);
                dto.userName = teacher.User.UserName;
                dto.Phone = teacher.User.PhoneNumber;
                dto.Email = teacher.User.Email;
                dto.FirstName = teacher.FirstName;
                dto.LastName = teacher.LastName;
                
                dtos.Add(dto);
            }
            
            return Ok(dtos);
        }

        [HttpGet("id:int")]
        public IActionResult GetTeacher(int id)
        {
            Teacher? teacher = context.Teachers
                .FirstOrDefault(t => t.Id == id);
            TeacherDetailsWithSubjectAndQuizesNamesDTO dto = new ();
            if(teacher is not null)
            {
                dto.Id = teacher.Id;
                dto.Address = teacher.Address;
                teacher.User = context.Users.FirstOrDefault(u => u.userId == teacher.Id);
                dto.Phone = teacher.User.PhoneNumber;
                dto.Email = teacher.User.Email;
                dto.FirstName = teacher.FirstName;
                dto.LastName = teacher.LastName;
            }
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, UpdateTeacherDto teacher)
        {
            if (id != teacher.Id)
            {
                return BadRequest("ID mismatch between route parameter and request body.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTeacher = context.Teachers.Find(id);
            existingTeacher.User = context.Users.FirstOrDefault(u => u.userId == id);
            if (existingTeacher == null)
            {
                return NotFound();
            }

            existingTeacher.FirstName = teacher.FirstName;
            existingTeacher.LastName = teacher.LastName;
            existingTeacher.User.Email = teacher.Email;
            existingTeacher.User.PhoneNumber = teacher.Phone;
            existingTeacher.ProfileImageUrl = teacher.ProfileImageUrl;
            existingTeacher.Address = teacher.Address;

            try
            {
                //context.Entry(existingTeacher).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
                {
                    return NotFound("Teacher Not Found");
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
            var user = context.Users.Single(u => u.userId == id);
            if (user == null)
            {
                return NotFound();
            }

            context.Users.Remove(user);

            context.SaveChanges();

            return NoContent();
        }

        private bool TeacherExists(int id)
        {
            return context.Teachers.Any(e => e.Id == id);
        }

    }

}
