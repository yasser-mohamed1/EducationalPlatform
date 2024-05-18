using EducationalPlatform.DTO;
using EducationalPlatform.Entities;

namespace EducationalPlatform.services
{
	public interface IQuestionServices
	{
		public Task<Question> GetQuestionById(int  id);
		public Task CreateQuestion(QuestionDto question ,int SubjectId);
		public Task DeleteQuestion(int QuestionId);
		public Task UpdateQuestion(Question question);
		public Task<bool>QuestionExists(int QuestionId);
		public Task<List<string>>GetTheCorrectAnswerForAQuestion(int QuestionId);

	}
}
