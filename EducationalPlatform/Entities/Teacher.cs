using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalPlatform.Entities
{
    public class Teacher
    {
        [ForeignKey("User")]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Governorate { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public virtual List<Subject>? Subjects { get; set; }
    }
}
