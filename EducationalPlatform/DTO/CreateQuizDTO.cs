using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.DTO
{
    public class CreateQuizDTO
    {
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public string Description { get; set; }
        
    }
}
