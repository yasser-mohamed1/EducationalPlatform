using EducationalPlatform.Data;
using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationalPlatform.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly EduPlatformContext _context;

        public QuestionRepository(EduPlatformContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuestionDto>> GetQuestionsAsync()
        {
            return await _context.Questiones.Select(q => new QuestionDto
            {
                Id = q.Id,
                Content = q.Content,
                option1 = q.Option1,
                option2 = q.Option2,
                option3 = q.Option3,
                option4 = q.Option4,
                QuizId = q.QuizId,
                CorrectAnswer = q.CorrectAnswer,
            }).ToListAsync();
        }

        public async Task<QuestionDto> GetQuestionByIdAsync(int id)
        {
            var question = await _context.Questiones.FindAsync(id);
            if (question == null)
            {
                return null;
            }

            return new QuestionDto
            {
                Id = question.Id,
                Content = question.Content,
                option1 = question.Option1,
                option2 = question.Option2,
                option3 = question.Option3,
                option4 = question.Option4,
                QuizId = question.QuizId,
                CorrectAnswer = question.CorrectAnswer,
            };
        }

        public async Task<IEnumerable<QuestionDto>> GetQuestionsByQuizIdAsync(int quizId)
        {
            return await _context.Questiones
                                 .Where(q => q.QuizId == quizId)
                                 .Select(q => new QuestionDto
                                 {
                                     Id = q.Id,
                                     Content = q.Content,
                                     option1 = q.Option1,
                                     option2 = q.Option2,
                                     option3 = q.Option3,
                                     option4 = q.Option4,
                                     QuizId = q.QuizId,
                                     CorrectAnswer = q.CorrectAnswer,
                                 }).ToListAsync();
        }

        public async Task<QuestionDto> CreateQuestionAsync(CreateQuestionDTO dto)
        {
            var question = new Question
            {
                Content = dto.Content,
                Option1 = dto.option1,
                Option2 = dto.option2,
                Option3 = dto.option3,
                Option4 = dto.option4,
                QuizId = dto.QuizId,
                CorrectAnswer = dto.CorrectAnswer,
            };

            _context.Questiones.Add(question);
            await _context.SaveChangesAsync();

            return new QuestionDto
            {
                Id = question.Id,
                Content = question.Content,
                option1 = question.Option1,
                option2 = question.Option2,
                option3 = question.Option3,
                option4 = question.Option4,
                QuizId = question.QuizId,
                CorrectAnswer = question.CorrectAnswer,
            };
        }

        public async Task UpdateQuestionAsync(int id, CreateQuestionDTO dto)
        {
            var question = await _context.Questiones.FindAsync(id);
            if (question == null)
            {
                return;
            }

            question.Content = dto.Content;
            question.Option1 = dto.option1;
            question.Option2 = dto.option2;
            question.Option3 = dto.option3;
            question.Option4 = dto.option4;
            question.CorrectAnswer = dto.CorrectAnswer;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(int id)
        {
            var question = await _context.Questiones.FindAsync(id);
            if (question != null)
            {
                _context.Questiones.Remove(question);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> QuizExistsAsync(int id)
        {
            return await _context.Quizzes.AnyAsync(e => e.Id == id);
        }
    }
}
