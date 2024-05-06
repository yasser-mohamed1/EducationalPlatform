using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.DTO
{
    public class RegisterAdminDto
    {
        [Required, StringLength(256)]
        public string Username { get; set; }

        [Required, StringLength(256)]
        public string Email { get; set; }

        [Required, RegularExpression("^\\+20\\d{10}$")]
        public string Phone { get; set; }

        [Required, StringLength(256)]
        public string Password { get; set; }

        [Compare("Password")]
        public string confirmPassword { get; set; }
    }
}
