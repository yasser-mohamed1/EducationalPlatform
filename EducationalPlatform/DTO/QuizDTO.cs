namespace EducationalPlatform.DTO
{
    public class QuizDto
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
