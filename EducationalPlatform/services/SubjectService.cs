using EducationalPlatform.Data;
using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;
namespace EducationalPlatform.services
{
	public class SubjectService : ISubjectServices
	{
		public EduPlatformContext Context;
		public SubjectService(EduPlatformContext _Context)
		{
			Context = _Context;
		}
		public async Task DeleteSubjectByIdAsync(int id)
		{
			bool exx = await Context.Subjects.AnyAsync(x => x.Id == id);
			if (!exx)
			{
				throw new Exception("The Subject Was not found");
			}
			else
			{
				Subject Subj = await Context.Subjects
										 .Include(c => c.Quizs)
										 .FirstOrDefaultAsync(c => c.Id == id);

				Context.Quizzes.RemoveRange(Subj.Quizs);
				Context.Subjects.Remove(Subj);
				try
				{
					await Context.SaveChangesAsync();
				}
				catch (Exception ex)
				{
					throw new Exception($"The Removing operation failed: {ex.Message}", ex);
				}

			}
		}

		public async Task<List<SubjectDto>> GetAllSubjectsForATeacher(int TeacherId)
		{
			Teacher Teacher = await Context.Teachers.Include(t => t.Subjects).FirstOrDefaultAsync(c => c.Id == TeacherId);
			if(Teacher == null)
			{
				return null;
			}
			else
			{
				var subjectDtos = Teacher.Subjects.Select(s => new SubjectDto
				{
					subjectId = s.Id,
					ProfileImageUrl = Teacher.ProfileImageUrl,
					subjName=s.subjName,
					AddingTime=s.AddingTime,
					Describtion=s.Describtion,
					Level=s.Level,
					pricePerHour=s.pricePerHour,
					TeacherId=TeacherId,
					TeacherName=Teacher.FirstName+" "+Teacher.LastName,
					Term=s.Term,
				}).ToList();
				return subjectDtos;
			}
			
		}


		public async Task<CreateSubjectDTO> CreateSubjectAsync(CreateSubjectDTO subject)
		{
			bool ex1 = await Context.Teachers.AnyAsync(c=>c.Id==subject.TeacherId);
			if (ex1)
			{
				Subject s = new Subject
				{
					pricePerHour = subject.pricePerHour,
					Level = subject.Level,
					subjName = subject.subjName,
					Describtion = subject.Describtion,
					TeacherId = subject.TeacherId,
					AddingTime = DateTime.Now,
					Term = subject.Term,
				};
				Context.Subjects.Add(s);
				try
				{
					await Context.SaveChangesAsync();
					return new CreateSubjectDTO
					{
						TeacherId = s.TeacherId,
						Describtion = s.Describtion,
						Level = s.Level,
						pricePerHour = s.pricePerHour,
						subjName = s.subjName,
						Term = s.Term,
					};
				}
				catch (Exception ex)
				{
					throw new Exception("The Operation Failed");
				}
			}
		  else
			{
				throw new Exception("Teacher Not Found");
			}
		}

		public async Task<SubjectDto> GetSubjectByIdAsync(int id)
		{
			Subject s = await Context.Subjects.Include(c=>c.Teacher).FirstOrDefaultAsync(x => x.Id == id);
			if (s != null && s.Teacher!=null)
			{
				return new SubjectDto
				{
					subjectId = s.Id,
					subjName = s.subjName,
					Describtion = s.Describtion,
					Level = s.Level,
					TeacherId = s.TeacherId,
					AddingTime =s.AddingTime,
					pricePerHour = s.pricePerHour,
					ProfileImageUrl = s.Teacher.ProfileImageUrl,
					TeacherName=s.Teacher.FirstName+" "+s.Teacher.LastName,
					Term=s.Term,
					
				};
			}
			else
			{ return null; }
		}

		public async Task<String> GetTeacherNameForAsubject(int id)
		{

			Subject s=await Context.Subjects.Include(e=>e.Teacher).FirstOrDefaultAsync(t=>t.Id==id);
			if (s != null)
			{
				string ss = s.Teacher.FirstName + " " + s.Teacher.LastName;
				return ss;
			}
			else
			{
				return "The Teacher Not Found";
			}
		}

		public bool SubjectExists(int id)
		{
			return Context.Students.Any(x => x.Id == id);
		}

		public async Task<TeacherBannerDto> GetTheTeacherForAsubject(int subid)
		{
			bool ex=await Context.Subjects.AnyAsync(x=>x.Id==subid);
			if (!ex) return null;
			else
			{
				Subject s=await Context.Subjects.Include(c=>c.Teacher).FirstOrDefaultAsync(x=>x.Id==subid);
				if (s != null)
				{
					Teacher T = s.Teacher;
					return new TeacherBannerDto
					{
						ProfileImageURl = T.ProfileImageUrl,
						FirstName = T.FirstName,
						LastName = T.LastName,
						Id = T.Id,

					};
				}
				else return null;
			}
		}

		public async Task UpdateSubjectByIdAsync(int SubjectId, UpdateSubjectDto ss)
		{
			bool Exists=await Context.Subjects.AnyAsync(c=>c.Id==SubjectId);
			if(!Exists)
			{
				throw new Exception("The Subject Was not Found");
			}
			else
			{
				Subject s=await Context.Subjects.FirstOrDefaultAsync(x=>x.Id == SubjectId);
				 if (s != null)
				{
					s.Describtion = ss.Describtion;
					s.Level = ss.Level;
					s.subjName = ss.subjName;
					s.Term = ss.Term;
					s.pricePerHour = ss.pricePerHour;
				}
				Context.Subjects.Update(s);
				try
				{
					await Context.SaveChangesAsync();
				}
				catch (Exception ex) {

					throw new Exception("The Operaion Failed");
				}
			}
		}
	}
}

