using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace EducationalPlatform.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string subjName { get; set; }
        public string Level { get; set; }
        public string Describtion { get; set; }
        public int pricePerHour { get; set; }

        [ForeignKey("Teacher")]
        [Required]
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public DateTime? AddingTime { get; set; }
        public virtual List<Quiz>? Quizs { get; set; } = new List<Quiz>();
        public virtual List<Enrollment>? Enrollments { get; set; }
		public int Term
		{
            set;
            get;
		}

	}


}
