using EducationalPlatform.Data;
using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
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
		public async Task<IActionResult> MakeEnrollment(int StudentId,int SubjectId)
		{
			if (ModelState.IsValid)
			{
				try
				{
					 Enrollment E=await EnrollmentServices.MakeEnrollment(StudentId,SubjectId);
					return Ok(new
					{
						success = true,
						message = "Enrollment created successfully.",
						data = E
					});
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
		[HttpGet("student/{studentId}")]
		public async Task<IActionResult> GetAllEnrollmentForAStudent(int studentId)
		{
			try
			{
				 List<EnrollmentDto>E= await EnrollmentServices.GetAllEnrollmentsForAstudent(studentId);
				return Ok(E);
			}
			catch (Exception ex )
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpDelete]
		public async Task<IActionResult>RemoveEnrollment(int Enrollmentid)
		{
			try
			{
				await EnrollmentServices.RemoveEnrollment(Enrollmentid);
				return Ok("The Enrollment Removed Successfully");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("subject/{SubjectId}")]
		public async Task<IActionResult> GetAllstudentsEnrolledInAsubject(int SubjectId)
		{
			try
			{
				List<StudentEnrollmentDto> ss = await EnrollmentServices.GetAllstudentsEnrolledInAsubject(SubjectId);
				return Ok(ss);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("{studentId}/{subjectId}")]
		public async Task<IActionResult> IsTheStudentEnrolledInThatSubject(int studentId,int subjectId)
		{
			bool IS=await EnrollmentServices.IsTheStudentEnrolledInThatSubject(studentId,subjectId);
			if (IS)
			{
				return Ok("The Student Enrolled In That Subject");
			}
			else
			{
				return BadRequest("The Student didn't enrolled in that subject");
			}
		}


	}
}
