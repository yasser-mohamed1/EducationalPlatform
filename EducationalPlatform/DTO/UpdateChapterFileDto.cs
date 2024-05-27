namespace EducationalPlatform.DTO
{
    public class UpdateChapterFileDto
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }
    }
}
