namespace EducationalPlatform.Dtos
{
    public class CreateChapterFileDto
    {
        public int ChapterId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }
    }
}
