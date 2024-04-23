namespace EducationalPlatform.Entities
{
	public class Enrollment
	{
		public int id { get; set; }
		public DateTime EnrollmentDate { get; set; }
		public DateTime ExpirationDate { get; set; }
		public string EnrollmentMethod { get; set; }
		public Subject Subject { get; set; }
		public int SubjectId { get; set; }
		public Student Student { get; set; }
		public int StudentId { get; set; }


	}
}
