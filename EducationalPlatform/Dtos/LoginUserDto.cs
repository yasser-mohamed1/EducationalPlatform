using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.Dtos
{
    public class LoginUserDto
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
