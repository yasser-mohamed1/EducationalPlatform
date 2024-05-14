namespace EducationalPlatform.Entities
{
	public class QuizStudent
	{
		public int QuizId { get; set; }
		public Quiz Quiz { get; set; }

		public int StudentId { get; set; }
		public Student Student { get; set; }	
	}
}
