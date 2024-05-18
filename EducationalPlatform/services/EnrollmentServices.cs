using EducationalPlatform.Data;
using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace EducationalPlatform.services
{
	//public class EnrollmentServices : IEnrollmentServices
	//{
	//	public EduPlatformContext Context;
	//	public ISubjectServices SubjectServices;
	//	public EnrollmentServices(EduPlatformContext _Context,ISubjectServices _SubjectServices) 
	//	{
	//	     Context=_Context;
	//		SubjectServices=_SubjectServices;
	//	}

		

	//	//public async Task<List<EnrollmentDtoWithSubjectNameAndTeacherName>> GetAllEnrollmentsForAstudent(int StudentId)
	//	//{
	//	//	List<Enrollment> E = await Context.Enrollments
	//	//									  .Where(e => e.StudentId == StudentId)
	//	//									  .Include(e => e.Subject)
	//	//									  .ToListAsync();

	//	//	List<EnrollmentDtoWithSubjectNameAndTeacherName> EST = new List<EnrollmentDtoWithSubjectNameAndTeacherName>();
	//	//	if (E.IsNullOrEmpty())
	//	//	{
	//	//		return EST;
	//	//	}
	//	//	else
	//	//	{
	//	//		foreach (Enrollment e in E)
	//	//		{
	//	//			EnrollmentDtoWithSubjectNameAndTeacherName tmp = new EnrollmentDtoWithSubjectNameAndTeacherName();
	//	//			tmp.SubjectName = e.Subject.subjName;
	//	//			tmp.EnrollmentMethod = e.EnrollmentMethod;
	//	//			tmp.TeacherName = await SubjectServices.GetTeacherNameForAsubject(e.SubjectIdd);
	//	//			tmp.ExpirationDate = e.ExpirationDate;
	//	//			tmp.EnrollmentDate = e.EnrollmentDate;
	//	//			EST.Add(tmp);
	//	//		}

	//	//		return EST;
	//	//	}
	//	//}


	//	public async Task<List<StudentDTO>> GetAllStudentsEnrolledInSubject(int SubjectId)
	//	{
	//		 var Query =await Context.Enrollments.AsSplitQuery()
	//			.Include(e => e.Student)
	//			.Where(e=>e.SubjectIdd == SubjectId) .ToListAsync();
	//		List<StudentDTO>Est = new List<StudentDTO>();
	//		if (Query.IsNullOrEmpty())
	//		{
	//			return Est;
	//		}
	//		else
	//		{
	//			foreach (var e in Query)
	//			{
	//				Est.Add(
	//					new StudentDTO
	//					{
	//						FirstName = e.Student.FirstName,
	//						LastName = e.Student.LastName,
	//						Level = e.Student.Level
	//					}
	//					) ;
	//			}
	//			return Est;
	//		}
	//	}

	//	public async Task<string> MakeEnrollment(EnrollmentDto dto)
	//	{
	//		var Student = await Context.Students.Include(c => c.Enrollments).
	//		   FirstOrDefaultAsync(c => c.Id == dto.StudentId);
	//		if (Student == null)
	//		{
	//			throw new Exception("The Student Not Found");
	//			return null;
	//		}
	//		var Subject = await Context.Subjects.FirstOrDefaultAsync(c => c.Id == dto.SubjectId);
	//		if (Subject == null)
	//		{
	//			throw new Exception("The Subject Not Found");
	//			return null;
	//		}
	//		Enrollment E = await Context.Enrollments.LastOrDefaultAsync(c => c.StudentId == dto.StudentId && c.SubjectIdd == dto.SubjectId);
	//		bool act;
	//		if (E != null)
	//		{
	//			act = await IsActive(E.ExpirationDate);
	//			if (act)
	//			{
	//				throw new Exception("You Have Already Enrolled In that Subject");
	//			}
	//			else
	//			{
	//				DateTime now = DateTime.Now;
	//				DateTime dateTimeAfter = now.AddMonths(1);

	//				var Enroll = new Enrollment
	//				{
	//					EnrollmentDate = now.ToString(),
	//					EnrollmentMethod = dto.EnrollmentMethod,
	//					ExpirationDate = dateTimeAfter.ToString(),
	//					Student = Student,
	//					Subject = Subject,
	//					StudentId = dto.StudentId,
	//					SubjectIdd = dto.SubjectId,

	//				};
	//				await Context.Enrollments.AddAsync(Enroll);

	//				try
	//				{
	//					await Context.SaveChangesAsync();
	//					return "Created Successfully";
	//				}
	//				catch
	//				{
	//					throw new Exception("Can't Make an Enrollment");
	//				}
	//			}
	//		}
	//		return null;
	//	}

	//	public async Task RemoveEnrollment(int EnrollmentId)
	//	{
	//		bool Ex=await Context.Enrollments.AnyAsync(E=>E.id == EnrollmentId);
	//		if (Ex)
	//		{
	//			Enrollment enrollment = await Context.Enrollments.FirstOrDefaultAsync(e => e.id == EnrollmentId);
	//			if (enrollment != null)
	//			{
	//				Context.Enrollments.Remove(enrollment);
	//				await Context.SaveChangesAsync();
	//			}
	//		}
	//		else
	//		{ throw new InvalidOperationException("This Enrollment Not Found"); }
	//	}
	//	public async Task<bool> IsActive(string date)
	//	{
	//		DateTime v;
	//		DateTime now = DateTime.Now;
	//		if (DateTime.TryParse(date, out v))
	//		{
	//			if (v <= now) return true;


	//			else
	//			{
	//				return false;
	//			}
	//		}
	//		return false;
	//	}
	//}
}
