using EducationalPlatform.DTO;
using EducationalPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet("{quizId}/questions")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuizQuestions(int quizId)
        {
            var questions = await _quizService.GetQuizQuestionsAsync(quizId);

            if (questions == null)
            {
                return NotFound($"No Quiz was found with this id: {quizId}");
            }

            return Ok(questions);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetQuizzes()
        {
            var quizzes = await _quizService.GetQuizzesAsync();
            return Ok(quizzes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizDto>> GetQuiz(int id)
        {
            var quiz = await _quizService.GetQuizByIdAsync(id);

            if (quiz == null)
            {
                return NotFound($"No Quiz was found with this id: {id}");
            }

            return Ok(quiz);
        }

        [HttpPost]
        public async Task<ActionResult<QuizDto>> CreateQuiz(CreateQuizDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var quiz = await _quizService.CreateQuizAsync(dto);
                return Ok(quiz);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuiz(int id, CreateQuizDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var quiz = await _quizService.UpdateQuizAsync(id, dto);
                return Ok(quiz);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            try
            {
                await _quizService.DeleteQuizAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
