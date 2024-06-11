using EducationalPlatform.DTO;

namespace EducationalPlatform.Services
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionDto>> GetQuestionsAsync();
        Task<QuestionDto> GetQuestionByIdAsync(int id);
        Task<IEnumerable<QuestionDto>> GetQuestionsByQuizIdAsync(int quizId);
        Task<QuestionDto> CreateQuestionAsync(CreateQuestionDTO dto);
        Task UpdateQuestionAsync(int id, CreateQuestionDTO dto);
        Task DeleteQuestionAsync(int id);
    }
}
