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
	public class EnrollmentServices : IEnrollmentServices
	{
		public EduPlatformContext Context;
		public ISubjectServices SubjectServices;
		public EnrollmentServices(EduPlatformContext _Context,ISubjectServices _SubjectServices) 
		{
		     Context=_Context;
			SubjectServices=_SubjectServices;
		}
		public async Task<List<EnrollmentDtoWithSubjectNameAndTeacherName>> GetAllEnrollmentsForAstudent(int StudentId)
		{
			List<Enrollment> E = await Context.Enrollments
											  .Where(e => e.StudentId == StudentId)
											  .Include(e => e.Subject)
											  .ToListAsync();

			List<EnrollmentDtoWithSubjectNameAndTeacherName> EST = new List<EnrollmentDtoWithSubjectNameAndTeacherName>();
			if (E.IsNullOrEmpty())
			{
				return EST;
			}
			else
			{
				foreach (Enrollment e in E)
				{
					EnrollmentDtoWithSubjectNameAndTeacherName tmp = new EnrollmentDtoWithSubjectNameAndTeacherName();
					tmp.SubjectName = e.Subject.subjName;
					tmp.EnrollmentMethod = e.EnrollmentMethod;
					tmp.TeacherName = await SubjectServices.GetTeacherNameForAsubject(e.SubjectIdd);
					tmp.ExpirationDate = e.ExpirationDate;
					tmp.EnrollmentDate = e.EnrollmentDate;
					EST.Add(tmp);
				}

				return EST;
			}
		}


		public async Task<List<StudentDTO>> GetAllStudentsEnrolledInSubject(int SubjectId)
		{
			 var Query =await Context.Enrollments.AsSplitQuery()
				.Include(e => e.Student)
				.Where(e=>e.SubjectIdd == SubjectId) .ToListAsync();
			List<StudentDTO>Est = new List<StudentDTO>();
			if (Query.IsNullOrEmpty())
			{
				return Est;
			}
			else
			{
				foreach (var e in Query)
				{
					Est.Add(
						new StudentDTO
						{
							FirstName = e.Student.FirstName,
							LastName = e.Student.LastName,
							Level = e.Student.Level
						}
						) ;
				}
				return Est;
			}
		}

		public async Task MakeEnrollment(int StudentId, EnrollmentDto Enrollmentt)
		{
			bool Ex= await Context.Students.AnyAsync(e=>e.Id == StudentId);
			bool Ex2=await Context.Subjects.AnyAsync(e=>e.Id==Enrollmentt.SubjectId);
			if (Ex && Ex2)
			{
				Enrollment E = new Enrollment();
				E.SubjectIdd=Enrollmentt.SubjectId;
				E.EnrollmentDate = Enrollmentt.EnrollmentDate;
				E.ExpirationDate = Enrollmentt.ExpirationDate;
				E.EnrollmentMethod = Enrollmentt.EnrollmentMethod;
				E.StudentId = StudentId;
			  try
				{
					Context.Enrollments.Add(E);
					await Context.SaveChangesAsync();
				}
				catch (Exception s)
				{
					throw new Exception($"Can't Saving in database due to {s}");
				}
			}
			else if(!Ex && Ex2)
			{
				throw new Exception("Threre Is no Student with that id");
			}
			else if(Ex&&!Ex2) 
			{
				throw new Exception("There Is No Subject With that id");
			}
			else
			{
				throw new Exception("You should put student and subject ids");
			}
		}

		public async Task RemoveEnrollment(int EnrollmentId)
		{
			bool Ex=await Context.Enrollments.AnyAsync(E=>E.id == EnrollmentId);
			if (Ex)
			{
				Enrollment enrollment = await Context.Enrollments.FirstOrDefaultAsync(e => e.id == EnrollmentId);
				if (enrollment != null)
				{
					Context.Enrollments.Remove(enrollment);
					await Context.SaveChangesAsync();
				}
			}
			else
			{ throw new InvalidOperationException("This Enrollment Not Found"); }
		}
	}
}
