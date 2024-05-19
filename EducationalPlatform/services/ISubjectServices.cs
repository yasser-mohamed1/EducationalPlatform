using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationalPlatform.services
{
	public interface ISubjectServices
	{
		public Task<List<SubjectDto >> GetAllSubjectsForATeacher(int TeacherId);
		public Task DeleteSubjectByIdAsync (int id);
		public Task UpdateSubjectByIdAsync(int id, UpdateSubjectDto ss);
		public Task<CreateSubjectDTO> CreateSubjectAsync(CreateSubjectDTO subject);
		public Task<SubjectDto> GetSubjectByIdAsync(int id);
        public Task<string> GetTeacherNameForAsubject(int id);
		public bool SubjectExists(int id);
		public Task<TeacherBannerDto> GetTheTeacherForAsubject(int subid);
	}
}
