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
		[HttpPost]
		public IActionResult AddSubject(SubjectDto Sub)
		{
			subjectServices.CreateSubject(Sub);
			return Ok(ModelState);
		}
	}
}
