using EducationalPlatform.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.DTO
{
	public class SubjectDto
	{
        public int subjectId { get; set; }
        public string subjName { get; set; }
		public string Level { get; set; }
		public string Describtion { get; set; }
		public int pricePerHour { get; set; }
		public int TeacherId { get; set; }
		public DateTime? AddingTime { get; set; }
	    public string ProfileImageUrl {  get; set; }
		public string TeacherName { get; set; }
		public int Term {  get; set; }

	}
}
