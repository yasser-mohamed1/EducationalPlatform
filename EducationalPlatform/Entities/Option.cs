namespace EducationalPlatform.Entities
{
	public class Option
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public Question Question { get; set; }
		public int QuestionId { get; set; }

	}
}
