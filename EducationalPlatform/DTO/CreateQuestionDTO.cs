using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.DTO
{
    public class CreateQuestionDTO
    {
        [Required]
        public int QuizId { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
