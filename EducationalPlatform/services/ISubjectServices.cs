using EducationalPlatform.DTO;
using EducationalPlatform.Entities;

namespace EducationalPlatform.services
{
	public interface ISubjectServices
	{
		public Task<List<SubjectDto >> GetAllSubjectAsync();
		public Task DeleteSubjectByIdAsync (int id);
		public Task UpdateSubjectByIdAsync(int id,SubjectDto ss);
		public Task CreateSubjectAsync(SubjectDto subject);
		public Task<SubjectDto> GetSubjectByIdAsync(int id);


	}
}
