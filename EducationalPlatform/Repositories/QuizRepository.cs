using EducationalPlatform.Data;
using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPlatform.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly EduPlatformContext _context;

        public QuizRepository(EduPlatformContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuizDto>> GetQuizzesAsync()
        {
            return await _context.Quizzes.Select(q => new QuizDto
            {
                Id = q.Id,
                SubjectId = (int)q.SubjectId,
                Description = q.Description,
                CreatedDate = q.CreatedDate
            }).ToListAsync();
        }

        public async Task<QuizDto> GetQuizByIdAsync(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);

            if (quiz == null)
            {
                return null;
            }

            return new QuizDto
            {
                Id = quiz.Id,
                SubjectId = (int)quiz.SubjectId,
                Description = quiz.Description,
                CreatedDate = quiz.CreatedDate
            };
        }

        public async Task<IEnumerable<QuestionDto>> GetQuizQuestionsAsync(int quizId)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.Id == quizId);

            if (quiz == null)
            {
                return null;
            }

            return quiz.Questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                QuizId = q.QuizId,
                Content = q.Content,
                option1 = q.Option1,
                option2 = q.Option2,
                option3 = q.Option3,
                option4 = q.Option4,
                CorrectAnswer = q.CorrectAnswer
            }).ToList();
        }

        public async Task<QuizDto> CreateQuizAsync(CreateQuizDTO dto)
        {
            var quiz = new Quiz
            {
                SubjectId = dto.SubjectId,
                Description = dto.Description,
                CreatedDate = DateTime.Now
            };

            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();

            return new QuizDto
            {
                Id = quiz.Id,
                SubjectId = (int)quiz.SubjectId,
                Description = quiz.Description,
                CreatedDate = quiz.CreatedDate
            };
        }

        public async Task<QuizDto> UpdateQuizAsync(int id, CreateQuizDTO dto)
        {
            var quiz = await _context.Quizzes.FindAsync(id);

            if (quiz == null)
            {
                return null;
            }

            quiz.Description = dto.Description;
            quiz.SubjectId = dto.SubjectId;

            await _context.SaveChangesAsync();

            return new QuizDto
            {
                Id = quiz.Id,
                SubjectId = (int)quiz.SubjectId,
                Description = quiz.Description,
                CreatedDate = quiz.CreatedDate
            };
        }

        public async Task DeleteQuizAsync(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);

            if (quiz != null)
            {
                _context.Quizzes.Remove(quiz);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> QuizExistsAsync(int id)
        {
            return await _context.Quizzes.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> SubjectExistsAsync(int id)
        {
            return await _context.Subjects.AnyAsync(x => x.Id == id);
        }
    }

}
