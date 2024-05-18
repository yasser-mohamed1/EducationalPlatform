using EducationalPlatform.Entities;
using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.DTO
{
	public class QuestionDto
	{
		public int Id { get; set; }
        public int QuizId { get; set; }
        public string Content { get; set; }
        public string option1 { get; set; }
		public string option2 { get; set; }
		public string option3 { get; set; }
		public string option4 { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
