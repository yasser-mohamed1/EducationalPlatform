using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.Dtos
{
    public class CreateQuestionDTO
    {
        [Required]
        public int QuizId { get; set; }
        [Required]
        public string Content { get; set; }
        public string option1 { get; set; }
        public string option2 { get; set; }
        public string option3 { get; set; }
        public string option4 { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
