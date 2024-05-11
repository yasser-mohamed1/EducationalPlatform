using EducationalPlatform.DTO;
using EducationalPlatform.Entities;

namespace EducationalPlatform.services
{
	public interface IEnrollmentServices
	{
		public Task MakeEnrollment(int StudentId,EnrollmentDto Enrollment);
		public Task RemoveEnrollment(int  EnrollmentId);
		public Task<List<StudentDTO>>GetAllStudentsEnrolledInSubject(int SubjectId);
		public Task<List<EnrollmentDtoWithSubjectNameAndTeacherName>> GetAllEnrollmentsForAstudent(int StudentId);

	}
}
