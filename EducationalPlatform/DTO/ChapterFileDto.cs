namespace EducationalPlatform.DTO
{
    public class ChapterFileDto
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }
    }
}
