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
		public EnrollmentServices(EduPlatformContext _Context, ISubjectServices _SubjectServices)
		{
			Context = _Context;
			SubjectServices = _SubjectServices;
		}

		public Task<List<StudentDTO>> GetAllStudentsEnrolledInSubject(int SubjectId)
		{
			throw new NotImplementedException();
		}

		public Task<bool> IsActive(string date)
		{
			throw new NotImplementedException();
		}

		public async Task<Enrollment> MakeEnrollment(int StudentId, int SubjectId)
		{
			Enrollment Enrollment=await 
				Context.Enrollments
				.OrderByDescending(x=>x.EnrollmentDate)
				.FirstOrDefaultAsync(x=>x.SubjectIdd==SubjectId&&x.StudentId==StudentId);
			if (Enrollment != null && Enrollment.IsActive==true)
			{
				throw new Exception("You Have Already Enrolled in That Subject");
			
			}
			
			else if ((Enrollment!=null && Enrollment.IsActive==false) || Enrollment==null )
			{
				Enrollment E = new Enrollment
				{
					EnrollmentDate = DateTime.Now,
					SubjectIdd = SubjectId,
					StudentId = StudentId,
					ExpirationDate = DateTime.Now.AddMonths(1),
					
				};
				 Context.Enrollments .Add(E);
				try
				{
					await Context.SaveChangesAsync();
					return E;
				}
				catch (Exception ex)
				{
					throw new Exception($"{ex.InnerException.Message}");
				}
			}

			return Enrollment;

		}

		public async Task RemoveEnrollment(int EnrollmentId)
		{
			Enrollment En = await Context.Enrollments.FindAsync(EnrollmentId);
			
			if(En==null)
			{
				throw new Exception("The Enrollment Not Found");
			}
			else if(En!=null && En.IsActive==true) 
			{
				Context.Enrollments.Remove(En);
				try
				{
					await Context.SaveChangesAsync();
				}
				catch (Exception ex)
				{
					throw new Exception(ex.InnerException.Message);
				}
			}
		}

	}
}
