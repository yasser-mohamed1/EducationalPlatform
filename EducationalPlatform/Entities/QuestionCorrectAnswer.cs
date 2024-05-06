using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.Entities
{
	public class QuestionCorrectAnswer
	{
		public Question Question { get; set; }
		[Key]
		public int QuestionId { get; set; }
		public int CorrectAnswer { get; set; }
	}
}
