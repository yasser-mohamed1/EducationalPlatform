using EducationalPlatform.Data;
using EducationalPlatform.DTO;
using EducationalPlatform.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace EducationalPlatform.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EnrollmentController : ControllerBase

	{
		EduPlatformContext Context;
		IEnrollmentServices EnrollmentServices;
		ISubjectServices SubjectServices;
		public EnrollmentController(EduPlatformContext _Context, IEnrollmentServices _EnrollmentServices, ISubjectServices subjectServices)
		{
			Context = _Context;
			EnrollmentServices = _EnrollmentServices;
			SubjectServices = subjectServices;
		}
		[HttpPost]
		public async Task<IActionResult> MakeEnrollment([FromForm]EnrollmentDto enrollment)
		{
			if (ModelState.IsValid)
			{
				try
				{
					 string s=await EnrollmentServices.MakeEnrollment(enrollment);
					return Ok(enrollment);
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
			else
			{
				return BadRequest();
			}
		}
		[HttpGet]
		public async Task<IActionResult> GetAllEnrollmentForAStudent(int subjectId)
		{
			try
			{
				 List<StudentDTO>E= await EnrollmentServices.GetAllStudentsEnrolledInSubject(subjectId);
				return Ok(E);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
