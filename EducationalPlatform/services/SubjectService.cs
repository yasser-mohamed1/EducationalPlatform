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
			var subject = await Context.Subjects.FirstOrDefaultAsync(x => x.Id == id);
			if(subject != null)
			{
				Context.Subjects.Remove(subject);
				Context.SaveChanges();
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
					Id = s.Id,
					ProfileImageURl=Teacher.ProfileImageUrl,
					subjName=s.subjName,
					AddingTime=(s.AddingTime).ToString(),
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


		

		public async Task<int> CreateSubjectAsync(CreateSubjectDTO subject)
		{
			Subject s = new Subject
			{
				pricePerHour = subject.pricePerHour,
				Level = subject.Level,
				subjName = subject.subjName,
				Describtion = subject.Describtion,
				TeacherId = subject.TeacherId
			};
			Context.Subjects.Add(s);
			await Context.SaveChangesAsync();
			return s.Id;
		}

		public async Task<SubjectDto> GetSubjectByIdAsync(int id)
		{
			Subject s = await Context.Subjects.Include(c=>c.Teacher).FirstOrDefaultAsync(x => x.Id == id);
			if (s != null && s.Teacher!=null)
			{
				return new SubjectDto
				{
					Id = s.Id,
					subjName = s.subjName,
					Describtion = s.Describtion,
					Level = s.Level,
					TeacherId = s.TeacherId,
					AddingTime =( s.AddingTime).ToString(),
					pricePerHour = s.pricePerHour,
					ProfileImageURl=s.Teacher.ProfileImageUrl,
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

		public Task UpdateSubjectByIdAsync(int id, SubjectDto ss)
		{
			throw new NotImplementedException();
		}
	}
}

