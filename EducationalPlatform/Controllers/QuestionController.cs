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
    public class QuestionsController : ControllerBase
    {
        private readonly EduPlatformContext _context;

        public QuestionsController(EduPlatformContext context)
        {
            _context = context;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestions()
        {
            var questions = await _context.Questiones.Select(q => new QuestionDto
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

            return Ok(questions);
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDto>> GetQuestion(int id)
        {
            var question = await _context.Questiones.FindAsync(id);

            if (question == null)
            {
                return NotFound($"No Question was found with this id : {id}");
            }

            var questionDto = new QuestionDto
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

            return Ok(questionDto);
        }

        // POST: api/Questions
        [HttpPost]
        public async Task<ActionResult<QuestionDto>> CreateQuestion(CreateQuestionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!QuizExists(dto.QuizId))
            {
                return NotFound($"No Quiz was found with this id : {dto.QuizId}");
            }

            var question = new Question
            {
                Content = dto.Content,
                Option1 = dto.option1,
                Option2 = dto.option2,
                Option3 = dto.option3,
                Option4 = dto.option4,
                QuizId = dto.QuizId,
                CorrectAnswer = dto.CorrectAnswer
            };

            _context.Questiones.Add(question);
            await _context.SaveChangesAsync();

            var questionDto = new QuestionDto
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

            return Ok(questionDto);
        }

        // PUT: api/Questions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, CreateQuestionDTO dto)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var question = await _context.Questiones.FindAsync(id);
            if (question == null)
            {
                return NotFound($"No Question was found with this id : {id}");
            }

            question.Content = dto.Content;
            question.Option1 = dto.option1;
            question.Option2 = dto.option2;
            question.Option3 = dto.option3;
            question.Option4 = dto.option4;
            question.CorrectAnswer = dto.CorrectAnswer;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _context.Questiones.FindAsync(id);
            if (question == null)
            {
                return NotFound($"No Question was found with this id : {id}");
            }

            _context.Questiones.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Questions/ByQuiz/5
        [HttpGet("ByQuiz/{quizId}")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestionsByQuizId(int quizId)
        {
            if (!QuizExists(quizId))
            {
                return NotFound($"No Quiz was found with this ID: {quizId}");
            }

            var questions = await _context.Questiones
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

            return Ok(questions);
        }


        private bool QuizExists(int id)
        {
            return _context.Quizzes.Any(e => e.Id == id);
        }
    }

}
