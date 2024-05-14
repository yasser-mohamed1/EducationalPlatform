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

        // GET: api/quiz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetQuizzes()
        {
            var quizzes = await _context.Quizzes.Select(q => new QuizDto
            {
                Id = q.Id,
                SubjectId = (int)q.SubjectId,
                Description = q.Description
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
                return NotFound();
            }

            var quizDto = new QuizDto
            {
                Id = quiz.Id,
                SubjectId = (int)quiz.SubjectId,
                Description = quiz.Description
            };

            return Ok(quizDto);
        }

        // POST: api/quiz
        [HttpPost]
        public async Task<ActionResult<QuizDto>> CreateQuiz(CreateQuizDTO dto)
        {
            var quiz = new Quiz
            {
                SubjectId = dto.SubjectId,
                Description = dto.Description
            };

            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();

            return Ok(dto);
        }

        // PUT: api/quiz/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuiz(int id, QuizDto quizDto)
        {
            if (id != quizDto.Id)
            {
                return BadRequest();
            }

            var quiz = new Quiz
            {
                Id = quizDto.Id,
                SubjectId = quizDto.SubjectId,
                Description = quizDto.Description
            };

            _context.Entry(quiz).State = EntityState.Modified;

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

            return NoContent();
        }

        // DELETE: api/quiz/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuizExists(int id)
        {
            return _context.Quizzes.Any(e => e.Id == id);
        }
    }
}
