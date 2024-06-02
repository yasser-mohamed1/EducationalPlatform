namespace EducationalPlatform.Entities
{
    public class Chapter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public virtual List<ChapterFile> ChapterFiles { get; set; } = [];
    }
}
