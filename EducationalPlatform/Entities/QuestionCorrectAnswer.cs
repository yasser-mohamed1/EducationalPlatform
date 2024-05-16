using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.Entities
{
	public class QuestionCorrectAnswer
	{
		public int QuestionId { get; set; }
		public Question Question { get; set; }

		public int OptionId { get; set; }
        public Option Option { get; set; }
    }
}