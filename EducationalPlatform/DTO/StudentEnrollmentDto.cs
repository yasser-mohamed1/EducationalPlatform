namespace EducationalPlatform.DTO
{
	public class StudentEnrollmentDto
	{
		public int Id { get; set; }
		public int StudentId { get; set; }
		public string EnrollmentDate { get; set; }
		public StudentDTO StDto { get; set; }

	}
}
