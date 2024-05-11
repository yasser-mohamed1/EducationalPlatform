using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalPlatform.Entities
{
    public class Student
    {
        [ForeignKey("User")]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? Level { get; set; }
        public virtual ApplicationUser User { get; set; }
      
        public List<Result>? Result { get; set; }
        public List<QuizStudent> quizStudents { get; set; }
        public virtual List<Enrollment>? Enrollments { get; set; }

    }
}
