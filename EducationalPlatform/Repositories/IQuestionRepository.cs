using EducationalPlatform.DTO;

namespace EducationalPlatform.Repositories
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<QuestionDto>> GetQuestionsAsync();
        Task<QuestionDto> GetQuestionByIdAsync(int id);
        Task<IEnumerable<QuestionDto>> GetQuestionsByQuizIdAsync(int quizId);
        Task<QuestionDto> CreateQuestionAsync(CreateQuestionDTO dto);
        Task UpdateQuestionAsync(int id, CreateQuestionDTO dto);
        Task DeleteQuestionAsync(int id);
        Task<bool> QuizExistsAsync(int id);
    }
}
