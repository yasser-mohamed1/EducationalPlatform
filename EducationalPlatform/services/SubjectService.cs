using EducationalPlatform.Data;
using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;
namespace EducationalPlatform.services
{
	public class SubjectService : ISubjectServices
	{
		public EduPlatformContext Context;
		public SubjectService( EduPlatformContext _Context)
		{
			Context = _Context;
		}

	

		

		public void DeleteSubjectByIdAsync(int id)
		{
			var subject = Context.Subjects.FirstOrDefault(x => x.Id == id);
			if(subject != null)
			{
				Context.Subjects.Remove(subject);
				Context.SaveChanges();
			}
		}

		public async Task<List<SubjectDto>> GetAllSubjectAsync()
		{
			List<SubjectDto> Subjects = Context.Subjects.Include(s => s.Teacher)
				.Select(s => new SubjectDto
				{
					Id = s.Id,
					pricePerHour = s.pricePerHour,
					Level = s.Level,
					TeacherId = s.Teacher.Id,
					Describtion = s.Describtion,
					AddingTime = s.AddingTime,
					subjName = s.subjName,
				}).ToList();

			return Subjects;
			// throw new NotImplementedException(); // This line is unnecessary and can be removed.
		}


		public Task UpdateSubjectByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public void CreateSubject(SubjectDto subject)
		{
			
			Subject s = new Subject();
			s.pricePerHour = subject.pricePerHour;
			s.Level = subject.Level;
			s.subjName = subject.subjName;
			s.AddingTime = subject.AddingTime;
			s.Describtion = subject.Describtion;
			s.TeacherId = subject.TeacherId;
			Context.Subjects.Add(s);
			Context.SaveChanges();
		   
		}

		Task ISubjectServices.DeleteSubjectByIdAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}

