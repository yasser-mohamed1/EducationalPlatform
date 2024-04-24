using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.DTO
{
    public class RegisterTeacherDto
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
        public string Phone { get; set; }

        [Required, RegularExpression(@"^(m|f|M|F)$")]
        public char gender { get; set; }

        [Required, StringLength(50)]
        public string Governorate { get; set; }

        [Required, StringLength(50)]
        public string City { get; set; }

        [Required, StringLength(50)]
        public string Address { get; set; }

        [Required, StringLength(256)]
        public string Password { get; set; }
        [Compare("Password")]
        public string confirmPassword { get; set; }
    }
}
