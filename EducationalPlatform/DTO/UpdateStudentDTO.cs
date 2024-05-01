using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.DTO
{
    public class UpdateStudentDTO
    {
        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        [RegularExpression("^\\+20\\d{10}$")]
        public string? Phone { get; set; }

        public string? Password { get; set; }

        public string? Level { get; set; }

        [MaxLength(500)]
        public string? ProfileImageUrl { get; set; }
    }
}
