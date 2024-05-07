using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using EducationalPlatform.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPlatform.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubjectController : ControllerBase
	{
		ISubjectServices subjectServices;
		public SubjectController(ISubjectServices _subjectServices)
		{
			subjectServices = _subjectServices;
		}
		[HttpPost("Add Subject")]
		public async Task<IActionResult> AddSubject(SubjectDto Sub)
		{
			if (ModelState.IsValid)
			{
				await subjectServices.CreateSubjectAsync(Sub);
				return Ok(Sub);
			}
			else
				return BadRequest();

		}
		[HttpGet("Get Subject By Id")]
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

		[HttpDelete("Delete Subject")]
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
		[HttpPost("Update subject")]
		public async Task<IActionResult>UpdateSubjectByid(int id, SubjectDto subject)
		{
			if (ModelState.IsValid)
			{
				await subjectServices.UpdateSubjectByIdAsync(id, subject);
				return Ok("Subject Updated");
			}
			return BadRequest();
		}
	}
}
