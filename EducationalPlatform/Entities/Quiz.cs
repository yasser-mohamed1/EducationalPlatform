using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalPlatform.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        [ForeignKey("Subject")]
        public int? SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public string Description { get; set; }
        public virtual List<Question>? Questions { get; set; } = new List<Question>();
        public virtual List<Student>? Students { get; set; }
    }
}
