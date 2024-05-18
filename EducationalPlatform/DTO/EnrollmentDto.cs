using EducationalPlatform.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.DTO
{
	public class EnrollmentDto
	{

		public string EnrollmentMethod { get; set; }
		public int SubjectId { get; set; }
		public int StudentId { get; set; }
	}
}
