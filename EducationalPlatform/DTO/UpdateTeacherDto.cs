using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.DTO
{
    public class UpdateTeacherDto
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, StringLength(256)]
        public string Email { get; set; }

        [Required, RegularExpression("^\\+20\\d{10}$")]
        public string Phone { get; set; }

        [Required, StringLength(50)]
        public string Address { get; set; }

        [Required, StringLength(256)]
        public string Password { get; set; }

        [Required, MaxLength(500)]
        public string ProfileImageUrl { get; set; }
    }
}
