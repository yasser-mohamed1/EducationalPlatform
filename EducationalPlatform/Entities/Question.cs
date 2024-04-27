using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalPlatform.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }
        [Required]
        public List<Quiz> quizzes { get; set; }
        public List<QuestionCorrectAnswer>? CorrectAnswers { get; set; }  
        public List<Option> Options { get; set; }
        public Answer Answer { get; set; }
    }
}
