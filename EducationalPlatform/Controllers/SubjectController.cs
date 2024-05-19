using EducationalPlatform.Data;
using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using EducationalPlatform.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EducationalPlatform.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubjectController : ControllerBase
	{
		ISubjectServices subjectServices;

        public EduPlatformContext _context { get; }

        public SubjectController(ISubjectServices _subjectServices, EduPlatformContext context)
		{
			subjectServices = _subjectServices;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetSubjects()
        {
            var subjects = await _context.Subjects.Select(subject => new SubjectDto
            {
                subjectId = subject.Id,
                subjName = subject.subjName,
                Level = subject.Level,
                Describtion = subject.Describtion,
                pricePerHour = subject.pricePerHour,
                TeacherId = subject.TeacherId,
                AddingTime = subject.AddingTime,
				ProfileImageUrl = subject.Teacher.ProfileImageUrl,
                TeacherName = subject.Teacher.FirstName + " " + subject.Teacher.LastName,
                Term = subject.Term
            }).ToListAsync();

            return Ok(subjects);
        }

        [HttpPost]
		public async Task<IActionResult> AddSubject([FromForm]CreateSubjectDTO Sub)
		{
			if (ModelState.IsValid)
			{
				try
				{
					CreateSubjectDTO s = await subjectServices.CreateSubjectAsync(Sub);
					return Ok(s);
				}
				catch(Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
			else
				return BadRequest();
		}

		[HttpGet("{TeacherId}/subjects")]
		public async Task<IActionResult> GetAllSubjectsForATeacher(int TeacherId)
		{
			if (ModelState.IsValid)
			{
			
				var Subs = await subjectServices.GetAllSubjectsForATeacher(TeacherId);
				if(Subs.IsNullOrEmpty())
				{
					return BadRequest("The Teacher Not Found");
				}
					else
				{
					return Ok(Subs);
				}
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetSubjectByIdAsync(int id)
		{
			SubjectDto s = await subjectServices.GetSubjectByIdAsync(id);
			if (s != null)
				return Ok(s);
			return BadRequest("The Subject Was not Found ");
		}
		

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSubject(int id)
		{
			try
			{
				await subjectServices.DeleteSubjectByIdAsync(id);
				return Ok("The Subject Removed Successfully ");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult>UpdateSubjectByid(int id, UpdateSubjectDto subject)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await subjectServices.UpdateSubjectByIdAsync(id, subject);
					return Ok("The Subject Updated Successfully");
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
			return BadRequest();
		}

        // GET: api/subject/{subjectId}/quizzes
        [HttpGet("{subjectId}/quizzes")]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetSubjectQuizzes(int subjectId)
        {
            var subject = await _context.Subjects
                .Include(s => s.Quizs)
                .FirstOrDefaultAsync(s => s.Id == subjectId);

            if (subject == null)
            {
                return NotFound("Subject not found");
            }

            var quizzes = subject.Quizs.Select(q => new QuizDto
            {
                Id = q.Id,
                SubjectId = (int)q.SubjectId,
                Description = q.Description,
				CreatedDate = q.CreatedDate
            });

            return Ok(quizzes);
        }

		[HttpGet("{subjectId}/Teacher",Name ="Get The Teacher Banner")]
		public async Task<IActionResult>GetTeacherForAsubject(int subjectId)
		{
			TeacherBannerDto T=await subjectServices.GetTheTeacherForAsubject(subjectId);
			if(T!=null)
			{
				return Ok(T);
			}
			else
			{
				return BadRequest("The Teacher Not Found");
			}
		}
    }
}
