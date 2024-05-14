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
    public class QuestionController : ControllerBase
    {
        private readonly EduPlatformContext _context;

        public QuestionController(EduPlatformContext context)
        {
            _context = context;
        }

        // GET: api/question
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestions()
        {
            var questions = await _context.Questiones.Select(q => new QuestionDto
            {
                Id = q.Id,
                Content = q.Content
            }).ToListAsync();

            return Ok(questions);
        }

        // GET: api/question/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDto>> GetQuestion(int id)
        {
            var question = await _context.Questiones.FindAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            var questionDto = new QuestionDto
            {
                Id = question.Id,
                Content = question.Content
            };

            return Ok(questionDto);
        }

        // POST: api/question
        [HttpPost]
        public async Task<ActionResult<QuestionDto>> CreateQuestion(CreateQuestionDTO dto)
        {
            var question = new Question
            {
                Content = dto.Content
            };

            

            _context.Questiones.Add(question);
            await _context.SaveChangesAsync();

            QuizQuestion quizQuestion = new();
            quizQuestion.QuizId = dto.QuizId;
            quizQuestion.QuestionId = question.Id;
            _context.QuizQuestions.Add(quizQuestion);
            await _context.SaveChangesAsync();
            question.QuizQuestions.Add(quizQuestion);
            return Ok(dto);
        }

        // PUT: api/question/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, QuestionDto questionDto)
        {
            if (id != questionDto.Id)
            {
                return BadRequest();
            }

            var question = new Question
            {
                Id = questionDto.Id,
                Content = questionDto.Content
            };

            _context.Entry(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
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

        // DELETE: api/question/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _context.Questiones.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questiones.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionExists(int id)
        {
            return _context.Questiones.Any(e => e.Id == id);
        }
    }

}
