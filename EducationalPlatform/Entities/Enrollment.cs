using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalPlatform.Entities
{
	public class Enrollment
	{
		public DateTime EnrollmentDate { get; set; }
		public DateTime ExpirationDate { get; set; }
		
		[ForeignKey("Subject")]
		public int SubjectIdd { get; set; }
		public Subject? Subject { get; set; }
		
		public Student? Student { get; set; }
		[ForeignKey("Student")]
		public int StudentId { get; set; }
	}
}
