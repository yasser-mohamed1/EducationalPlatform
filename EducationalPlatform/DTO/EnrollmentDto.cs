using EducationalPlatform.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.DTO
{
	public class EnrollmentDto
	{
		public DateTime EnrollmentDate { get; set; }
		public DateTime ExpirationDate { get; set; }
		public string EnrollmentMethod { get; set; }
		public int SubjectId { get; set; }
		public int StudentId { get; set; }
	}
}
