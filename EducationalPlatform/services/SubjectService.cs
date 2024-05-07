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

		public async Task<List<SubjectDto>> GetAllSubjectAsync()
		{
			List<SubjectDto> Subjects =await Context.Subjects.Include(s => s.Teacher)
				.Select(s => new SubjectDto
				{
					
					pricePerHour = s.pricePerHour,
					Level = s.Level,
					TeacherId = s.Teacher.Id,
					Describtion = s.Describtion,
					AddingTime = s.AddingTime,
					subjName = s.subjName,
				}).ToListAsync();

			return Subjects;
			// throw new NotImplementedException(); // This line is unnecessary and can be removed.
		}

		
		public async Task UpdateSubjectByIdAsync(int id,SubjectDto ss)
		{
			Subject s= await Context.Subjects.FirstOrDefaultAsync(s => s.Id == id);
			if(s != null)
			{
				s.pricePerHour = ss.pricePerHour;
				s.Level = ss.Level;
				s.AddingTime = ss.AddingTime;
				s.Describtion= ss.Describtion;
				s.subjName	= ss.subjName;
				await Context.SaveChangesAsync();				
			}
			else
			{
				throw new KeyNotFoundException($"Subject with ID {id} not found.");
			}
		}

		public async Task CreateSubjectAsync(SubjectDto subject)
		{

			Subject s = new Subject
			{
				pricePerHour = subject.pricePerHour,
				Level = subject.Level,
				subjName = subject.subjName,
				AddingTime = subject.AddingTime,
				Describtion = subject.Describtion,
				TeacherId = subject.TeacherId
			};
			Context.Subjects.Add(s);
			await Context.SaveChangesAsync();
		   
		}

		public async Task<SubjectDto> GetSubjectByIdAsync(int id)
		{
			Subject s= await Context.Subjects.FirstOrDefaultAsync(x => x.Id == id);
			if(s != null)
			{
				return new SubjectDto
				{
					
					subjName = s.subjName,
					Describtion = s.Describtion,
					Level = s.Level,
					TeacherId = s.TeacherId,
					AddingTime = s.AddingTime,
					pricePerHour = s.pricePerHour,
				};
			}
			else
			{ return null; }	
		}
	}
}

