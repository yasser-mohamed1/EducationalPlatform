using EducationalPlatform.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.DTO
{
	public class EnrollmentDto
	{
		public int Id { get; set; }
		public int SubjectId { get; set; }
		public int StudentId { get; set; }
		public string ExpirationDate { get; set; }	
		public string EnrollmentDate { get; set; }
		public bool IsActive { get; set; }

	}
}
