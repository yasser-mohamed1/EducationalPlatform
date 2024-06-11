using EducationalPlatform.DTO;
using EducationalPlatform.Repositories;

namespace EducationalPlatform.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<IEnumerable<QuestionDto>> GetQuestionsAsync()
        {
            return await _questionRepository.GetQuestionsAsync();
        }

        public async Task<QuestionDto> GetQuestionByIdAsync(int id)
        {
            return await _questionRepository.GetQuestionByIdAsync(id);
        }

        public async Task<IEnumerable<QuestionDto>> GetQuestionsByQuizIdAsync(int quizId)
        {
            return await _questionRepository.GetQuestionsByQuizIdAsync(quizId);
        }

        public async Task<QuestionDto> CreateQuestionAsync(CreateQuestionDTO dto)
        {
            if (!await _questionRepository.QuizExistsAsync(dto.QuizId))
            {
                throw new KeyNotFoundException($"No Quiz was found with this id: {dto.QuizId}");
            }

            return await _questionRepository.CreateQuestionAsync(dto);
        }

        public async Task UpdateQuestionAsync(int id, CreateQuestionDTO dto)
        {
            if (!await _questionRepository.QuizExistsAsync(dto.QuizId))
            {
                throw new KeyNotFoundException($"No Quiz was found with this id: {dto.QuizId}");
            }

            await _questionRepository.UpdateQuestionAsync(id, dto);
        }

        public async Task DeleteQuestionAsync(int id)
        {
            await _questionRepository.DeleteQuestionAsync(id);
        }
    }
}
