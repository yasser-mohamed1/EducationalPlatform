using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.DTO
{
    public class RegisterStudentDto
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, StringLength(256)]
        public string Username { get; set; }

        [Required, StringLength(256)]
        public string Email { get; set; }

        [Required, RegularExpression("^\\+20\\d{10}$")]
        public string Phone { get ; set; }

        [Required, StringLength(100)]
        public string Level { get; set; }

        [Required, StringLength(256)]
        public string Password { get; set; }

        public IFormFile? ProfileImage { get; set; }
    }
}
