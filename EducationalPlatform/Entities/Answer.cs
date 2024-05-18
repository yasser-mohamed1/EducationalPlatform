using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalPlatform.Entities
{
    public class Answer
    {
        public int Id { get; set; }
       public string StudentAnswer { get; set; }
        [Required]
        public int QuestionId { get; set; }
          
        public Question Question { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        public AnswerResult AnswerResult { get; set; }

    }
}