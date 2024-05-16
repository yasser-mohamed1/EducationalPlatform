using System.Reflection.PortableExecutable;

namespace EducationalPlatform.DTO
{
    public class SearchSubjectDto
    {
        public int Id { get; set; }
        public string subjName { get; set; }
        public string Level { get; set; }
        public string Describtion { get; set; }
        public int pricePerHour { get; set; }
        public string teacherName { get; set; }
        public string profileImageUrl { get; set; }
        public int TeacherId { get; internal set; }
    }
}
