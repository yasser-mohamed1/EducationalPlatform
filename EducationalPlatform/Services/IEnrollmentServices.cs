using EducationalPlatform.Dtos;
using EducationalPlatform.Entities;

namespace EducationalPlatform.Services
{
	public interface IEnrollmentServices
	{
		public Task<Enrollment> MakeEnrollment(int StudentId,int SubjectId);
		public Task RemoveEnrollment(int  EnrollmentId);
		public Task<List<EnrollmentDto>>GetAllEnrollmentsForAstudent(int StudentId);
		public Task<List<StudentEnrollmentDto>> GetAllstudentsEnrolledInAsubject(int subjectId);
		public Task<bool>IsTheStudentEnrolledInThatSubject(int studentId,int subjectid);

	}
}
