using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<String>Phones { get; set; }

        public char gender { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
      
        public virtual List<Subject>? Subjects { get; set; } 
 
    }
}
