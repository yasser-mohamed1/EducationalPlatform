using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.DTO
{
    public class UpdateTeacherDto
    {
        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        [RegularExpression("^\\+20\\d{10}$")]
        public string? Phone { get; set; }

        public string? Address { get; set; }

        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d\\s]).{8,}$")]
        public string? Password { get; set; }

        public IFormFile? ProfileImage { get; set; }
    }
}
