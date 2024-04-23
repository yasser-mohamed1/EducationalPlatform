using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.Entities
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string userName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public char ?gender { get; set; }
        public string? Level { get; set; }
        public string? Address { get; set; }
        public List<Subject> Subjects { get; set; } = new List<Subject>();
        public List<Quiz>? Quizs { get; set; }
        public List<Result>? Result { get; set; }
    }
}
