using EducationalPlatform.Data;
using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using EducationalPlatform.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		[HttpPost]
		public async Task<IActionResult> AddSubject(CreateSubjectDTO Sub)
		{
			if (ModelState.IsValid)
			{
				await subjectServices.CreateSubjectAsync(Sub);
				return Ok(Sub);
			}
			else
				return BadRequest();
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetSubjectByIdAsync(int id)
		{
			SubjectDto s = await subjectServices.GetSubjectByIdAsync(id);
			if (s != null)
				return Ok(s);
			return BadRequest("The Subject Was not Found ");
		}
		[HttpGet]
		public async Task<IActionResult> GetAllSubject()
		{
			if (ModelState.IsValid)
			{
				List<SubjectDto> Allsub = await subjectServices.GetAllSubjectAsync();

				return Ok(Allsub);
			}
			return BadRequest();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSubject(int id)
		{
			SubjectDto s=await subjectServices.GetSubjectByIdAsync(id);
			if(s == null)
				return BadRequest("Not found");
			else
			{
				await subjectServices.DeleteSubjectByIdAsync(id);
				return Ok(s);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult>UpdateSubjectByid(int id, SubjectDto subject)
		{
			if (ModelState.IsValid)
			{
				await subjectServices.UpdateSubjectByIdAsync(id, subject);
				return Ok("Subject Updated");
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
                Description = q.Description
            });

            return Ok(quizzes);
        }
    }
}
