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
            List<Teacher> teachers = context.Teachers.Include(p=>p.Subjects).ToList();


            List<TeacherDetailsWithoutSubjectAndQuizesNamesDTO> dtos = [];

            foreach(var teacher in teachers)
            {
                TeacherDetailsWithoutSubjectAndQuizesNamesDTO dto
                = new TeacherDetailsWithoutSubjectAndQuizesNamesDTO();
                dto.Id = teacher.Id;
                dto.Address = teacher.Address;
                dto.Phones = teacher.Phones;
                
                dto.Email = teacher.Email;
                dto.FirstName = teacher.FirstName;
                dto.LastName = teacher.LastName;
                
                dto.gender = teacher.gender;
                dtos.Add(dto);
            }
            return Ok(dtos);
        }

        [HttpGet("id:int")]
        public IActionResult GetTeacher(int id)
        {
            Teacher? teacher = context.Teachers
                .FirstOrDefault(t => t.Id == id);
            TeacherDetailsWithSubjectAndQuizesNamesDTO dto
                = new TeacherDetailsWithSubjectAndQuizesNamesDTO();
            if(teacher is not null)
            {
                dto.Id = teacher.Id;
                dto.Address = teacher.Address;
                dto.Phones = teacher.Phones;
               
                dto.Email = teacher.Email;
                dto.FirstName = teacher.FirstName;
                dto.LastName = teacher.LastName;
                
                dto.gender = teacher.gender;
            }
            return Ok(dto);
        }
    }
}
