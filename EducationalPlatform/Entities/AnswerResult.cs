using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.Entities
{
	public class AnswerResult
	{
		public Result Result { get; set; }
		public int? ResultId { get; set; }
		[Key]
		public int AnswerId { get; set; }
		public Answer Answer { get; set; }
	}
}
