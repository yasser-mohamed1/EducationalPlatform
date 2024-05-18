using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalPlatform.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }
        
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        [ForeignKey("Quiz")]
        public int QuizId { get; set; }

        public Quiz Quiz { get; set; }
        public string CorrectAnswer { get; set; }
        public Answer Answer { get; set; }
    }
}
