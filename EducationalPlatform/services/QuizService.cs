using EducationalPlatform.DTO;
using EducationalPlatform.Repositories;

namespace EducationalPlatform.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<IEnumerable<QuizDto>> GetQuizzesAsync()
        {
            return await _quizRepository.GetQuizzesAsync();
        }

        public async Task<QuizDto> GetQuizByIdAsync(int id)
        {
            return await _quizRepository.GetQuizByIdAsync(id);
        }

        public async Task<IEnumerable<QuestionDto>> GetQuizQuestionsAsync(int quizId)
        {
            return await _quizRepository.GetQuizQuestionsAsync(quizId);
        }

        public async Task<QuizDto> CreateQuizAsync(CreateQuizDTO dto)
        {
            if (!await _quizRepository.SubjectExistsAsync(dto.SubjectId))
            {
                throw new KeyNotFoundException($"No Subject was found with this id: {dto.SubjectId}");
            }

            return await _quizRepository.CreateQuizAsync(dto);
        }

        public async Task<QuizDto> UpdateQuizAsync(int id, CreateQuizDTO dto)
        {
            if (!await _quizRepository.QuizExistsAsync(id))
            {
                throw new KeyNotFoundException($"No Quiz was found with this id: {id}");
            }

            return await _quizRepository.UpdateQuizAsync(id, dto);
        }

        public async Task DeleteQuizAsync(int id)
        {
            if (!await _quizRepository.QuizExistsAsync(id))
            {
                throw new KeyNotFoundException($"No Quiz was found with this id: {id}");
            }

            await _quizRepository.DeleteQuizAsync(id);
        }
    }
}
