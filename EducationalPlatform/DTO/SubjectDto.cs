using EducationalPlatform.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.DTO
{
	public class SubjectDto
	{
        public int Id { get; set; }
		
        public string subjName { get; set; }
		public string Level { get; set; }
		public string Describtion { get; set; }
		public int pricePerHour { get; set; }
		public int TeacherId { get; set; }
		public string? AddingTime { get; set; }
	    public string ProfileImageURl {  get; set; }
		public string TeacherName { get; set; }

	}
}
