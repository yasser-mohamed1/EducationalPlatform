using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.Entities
{
	public class Result
	{
		public int Id { get; set; }
		public int QuizId { get; set; }
		public Quiz Quiz { get; set; }
		public int Score { get; set; }
		[Required]
		public int StudentId { get; set; }	
		public Student Student { get; set; }
		public List<AnswerResult> AnswerResults { get; set; } 

	}
}
