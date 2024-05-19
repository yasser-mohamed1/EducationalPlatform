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

		public async Task<List<EnrollmentDto>> GetAllEnrollmentsForAstudent(int StudentId)
		{
			bool exists=await Context.Students.AnyAsync(c=>c.Id==StudentId);
			if (exists)
			{
				Student s = await Context.Students.Include(c => c.Enrollments).FirstOrDefaultAsync(c => c.Id == StudentId);
			
				
				List<Enrollment> Enrs = s.Enrollments;
				if (Enrs.IsNullOrEmpty())
				{
					throw new Exception("The student didn't make any enrollment");
				}
				else
				{
					List<EnrollmentDto> enrollmentDtos = Enrs.Select(enrollment =>
					new EnrollmentDto
					{
						Id = enrollment.Id,
						EnrollmentDate = (enrollment.EnrollmentDate).ToString(),
						ExpirationDate = (enrollment.ExpirationDate).ToString(),
						IsActive = enrollment.IsActive,
						StudentId = StudentId,
						SubjectId = enrollment.SubjectIdd,

					}).ToList();
					return enrollmentDtos;

				}
			}
			else
			{
				throw new Exception("The Student Not Found");
			}
		}

		public async Task<List<StudentEnrollmentDto>> GetAllstudentsEnrolledInAsubject(int subjectId)
		{
			bool Ex=await Context.Subjects.AnyAsync(c=>c.Id==subjectId);
			if(!Ex)
			{
				throw new Exception("The Subject Not Found");
			}
			else
			{
				Subject subject = await Context.Subjects.Include(c=>c.Enrollments).FirstOrDefaultAsync(c=>c.Id==subjectId);
				List<Enrollment> Enrs = subject.Enrollments;
				if(Enrs.IsNullOrEmpty())
				{
					throw new Exception("the Subject Doesn't have Any Enrollment");
				}
				else
				{
					
					List<StudentEnrollmentDto>Stu= Enrs.Select(enrollment =>
					new StudentEnrollmentDto
					{
						Id= enrollment.Id,
						EnrollmentDate=(enrollment.EnrollmentDate).ToString(),
						StudentId=enrollment.StudentId,

					}).ToList();
					foreach(var i in Stu)
					{
						i.StDto = await GetStudent(i.StudentId);
					}
					return Stu;
				}
			}
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
		public async Task<StudentDTO> GetStudent(int StudentId)
		{
			Student student = await Context.Students.FindAsync(StudentId);
			if (student==null)
			{
				return null;
			}
			else
			{
				StudentDTO s = new StudentDTO
				{
					Id = student.Id,
					ProfileImageUrl = student.ProfileImageUrl,
					FirstName = student.FirstName,
					LastName = student.LastName,
					Level = student.Level
				};
				return s;
			}
		}

		public async Task<bool> IsTheStudentEnrolledInThatSubject(int studentId, int subjectid)
		{
			List<EnrollmentDto>Es=await GetAllEnrollmentsForAstudent(studentId);
			if (Es==null) { return false; }
			else
			{
				foreach(var i in Es)
				{
					if(i.IsActive==true && i.SubjectId==subjectid)
					{
						return true;
						break;
					}
				}
			}
			return false;
		}
	}
}
