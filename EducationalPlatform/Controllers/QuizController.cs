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
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.Id == quizId);

            if (quiz == null)
            {
                return NotFound($"No Quiz was found with this id : {quizId}");
            }

            var questions = quiz.Questions.Select(q => new QuestionDto
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
                return NotFound($"No Quiz was found with this id : {id}");
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

            if (!SubjectExists(dto.SubjectId))
            {
                return NotFound($"No Subject was found with this id : {dto.SubjectId}");
            }

            var quiz = new Quiz
            {
                SubjectId = dto.SubjectId,
                Description = dto.Description,
                CreatedDate = DateTime.Now
            };

            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();

            return Ok(new
                {
                    id = quiz.Id,
                    subjectId = dto.SubjectId,
                    description = dto.Description,
                    createdDate = quiz.CreatedDate
                }
            );
        }

        // PUT: api/quiz/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuiz(int id, CreateQuizDTO dto)
        {
            if (!QuizExists(id))
            {
                return NotFound($"No Quiz was found with this id : {id}");
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
            if (quiz.SubjectId != null || quiz.SubjectId != 0)
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

            return Ok(new
            {
                id= quiz.Id,
                subjectId= quiz.SubjectId,
                description= quiz.Description,
                createdDate = quiz.CreatedDate
            });
        }

        // DELETE: api/quiz/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            if (!QuizExists(id))
            {
                return NotFound($"No Quiz was found with this id : {id}");
            }

            var quiz = await _context.Quizzes.FindAsync(id);
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
            return _context.Subjects.Any(x => x.Id == id);
        }
    }
}

