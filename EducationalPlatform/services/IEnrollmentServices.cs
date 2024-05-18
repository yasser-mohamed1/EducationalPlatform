using EducationalPlatform.DTO;
using EducationalPlatform.Entities;

namespace EducationalPlatform.services
{
	public interface IEnrollmentServices
	{
		public Task<Enrollment> MakeEnrollment(int StudentId,int SubjectId);
		public Task RemoveEnrollment(int  EnrollmentId);
		public Task<List<StudentDTO>>GetAllStudentsEnrolledInSubject(int SubjectId);

		public Task<bool> IsActive(string date);
		
		//public Task<List<EnrollmentDtoWithSubjectNameAndTeacherName>> GetAllEnrollmentsForAstudent(int StudentId);

	}
}
