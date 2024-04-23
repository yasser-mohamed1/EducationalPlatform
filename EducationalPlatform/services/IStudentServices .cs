using EducationalPlatform.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPlatform.services
{
	public interface IStudentServices
	{
		[HttpGet]
		Task<List<Quiz>> GetAllQuizzesAsc(int id);

		Task<Answer> GetStuResult(int id);
	}
}
