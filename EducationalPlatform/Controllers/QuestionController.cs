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
                return NotFound($"No Question was found with this : {id}");
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
        //public async Task<ActionResult<QuestionDto>> CreateQuestion(CreateQuestionDTO dto)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (!QuizExists(dto.QuizId))
        //    {
        //        return NotFound($"No Quiz was found with this : {dto.QuizId}");
        //    }

        //    var question = new Question
        //    {
        //        Content = dto.Content
        //    };
            
        //    _context.Questiones.Add(question);
        //    await _context.SaveChangesAsync();

        //    QuizQuestion quizQuestion = new();
        //    quizQuestion.QuizId = dto.QuizId;
        //    quizQuestion.QuestionId = question.Id;
        //    _context.QuizQuestions.Add(quizQuestion);
        //    question.QuizQuestions.Add(quizQuestion);
        //    await _context.SaveChangesAsync();
        //    return Ok(dto);
        //}

        // PUT: api/question/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, CreateQuestionDTO questionDto)
        {
            if (!QuestionExists(id))
            {
                return NotFound($"No Question was found with this : {id}");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Question? question = await _context.Questiones.FindAsync(id);

            question.Content = questionDto.Content;

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
                return NotFound($"No Question was found with this : {id}");
            }

            _context.Questiones.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionExists(int id)
        {
            return _context.Questiones.Any(e => e.Id == id);
        }

        private bool QuizExists(int id)
        {
            return _context.Quizzes.Any(e => e.Id == id);
        }
    }

}
