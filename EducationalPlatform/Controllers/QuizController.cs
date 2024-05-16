using EducationalPlatform.Data;
using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly EduPlatformContext _context;

        public QuizController(EduPlatformContext context)
        {
            _context = context;
        }

        // GET: api/quiz/{quizId}/questions
        [HttpGet("{quizId}/questions")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuizQuestions(int quizId)
        {

            var quiz = await _context.Quizzes
                .Include(q => q.QuizQuestions)
                .ThenInclude(qq => qq.Question)
                .FirstOrDefaultAsync(q => q.Id == quizId);

            if (quiz == null)
            {
                return NotFound("Quiz not found");
            }

            var questions = quiz.QuizQuestions.Select(qq => new QuestionDto
            {
                Id = qq.Question.Id,
                Content = qq.Question.Content,
                QuizId = qq.Quiz.Id
            }).ToList();

            return Ok(questions);
        }

        // GET: api/quiz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetQuizzes()
        {
            var quizzes = await _context.Quizzes.Select(q => new QuizDto
            {
                Id = q.Id,
                SubjectId = (int)q.SubjectId,
                Description = q.Description,
                CreatedDate = q.CreatedDate
            }).ToListAsync();

            return Ok(quizzes);
        }

        // GET: api/quiz/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizDto>> GetQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);

            if (quiz == null)
            {
                return NotFound($"No Quiz with found with this : {id}");
            }

            var quizDto = new QuizDto
            {
                Id = quiz.Id,
                SubjectId = (int)quiz.SubjectId,
                Description = quiz.Description,
                CreatedDate = quiz.CreatedDate
            };

            return Ok(quizDto);
        }

        // POST: api/quiz
        [HttpPost]
        public async Task<ActionResult<QuizDto>> CreateQuiz(CreateQuizDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(!SubjectExists(dto.SubjectId))
            {
                return NotFound($"No Subject with found with this : {dto.SubjectId}");
            }

            var quiz = new Quiz
            {
                SubjectId = dto.SubjectId,
                Description = dto.Description,
                CreatedDate = DateTime.Now
            };

            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();

            return Ok(dto);
        }

        // PUT: api/quiz/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuiz(int id, CreateQuizDTO dto)
        {
            if (!QuizExists(id))
            {
                return NotFound($"No Quiz with found with this : {id}");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Quiz? quiz = await _context.Quizzes.FindAsync(id);

            if (!string.IsNullOrEmpty(quiz.Description)) 
            { 
                quiz.Description = dto.Description;
            }
            if(quiz.SubjectId != null || quiz.SubjectId != 0)
                quiz.SubjectId = dto.SubjectId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(quiz);
        }

        // DELETE: api/quiz/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
            {
                return NotFound($"No Quiz with found with this : {id}");
            }

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuizExists(int id)
        {
            return _context.Quizzes.Any(e => e.Id == id);
        }

        private bool SubjectExists(int id)
        {
            return _context.Students.Any(x => x.Id == id);
        }
    }
}
