using EducationalPlatform.DTO;

namespace EducationalPlatform.Repositories
{
    public interface IQuizRepository
    {
        Task<IEnumerable<QuizDto>> GetQuizzesAsync();
        Task<QuizDto> GetQuizByIdAsync(int id);
        Task<IEnumerable<QuestionDto>> GetQuizQuestionsAsync(int quizId);
        Task<QuizDto> CreateQuizAsync(CreateQuizDTO dto);
        Task<QuizDto> UpdateQuizAsync(int id, CreateQuizDTO dto);
        Task DeleteQuizAsync(int id);
        Task<bool> QuizExistsAsync(int id);
        Task<bool> SubjectExistsAsync(int id);
    }
}
