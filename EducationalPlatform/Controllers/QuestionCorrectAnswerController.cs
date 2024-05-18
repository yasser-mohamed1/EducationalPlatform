using EducationalPlatform.Data;
using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalPlatform.Controllers
{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class QuestionCorrectAnswerController : ControllerBase
//    {
//        private readonly EduPlatformContext _context;

//        public QuestionCorrectAnswerController(EduPlatformContext context)
//        {
//            _context = context;
//        }

//        // GET: api/QuestionCorrectAnswer
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<QuestionCorrectAnswerDto>>> GetQuestionCorrectAnswers()
//        {
//            var questionCorrectAnswers = await _context.QuestionCorrectAnswers
//                .Select(qca => new QuestionCorrectAnswerDto
//                {
//                    QuestionId = qca.QuestionId,
//                    OptionId = qca.OptionId
//                })
//                .ToListAsync();

//            return Ok(questionCorrectAnswers);
//        }

//        // GET: api/QuestionCorrectAnswer/{questionId}/{optionId}
//        [HttpGet("{questionId}/{optionId}")]
//        public async Task<ActionResult<QuestionCorrectAnswerDto>> GetQuestionCorrectAnswer(int questionId, int optionId)
//        {
//            var questionCorrectAnswer = await _context.QuestionCorrectAnswers
//                .FirstOrDefaultAsync(qca => qca.QuestionId == questionId && qca.OptionId == optionId);

//            if (questionCorrectAnswer == null)
//            {
//                return NotFound("QuestionCorrectAnswer not found");
//            }

//            var questionCorrectAnswerDto = new QuestionCorrectAnswerDto
//            {
//                QuestionId = questionCorrectAnswer.QuestionId,
//                OptionId = questionCorrectAnswer.OptionId
//            };

//            return Ok(questionCorrectAnswerDto);
//        }

//        // POST: api/QuestionCorrectAnswer
//        [HttpPost]
//        public async Task<ActionResult<QuestionCorrectAnswerDto>> CreateQuestionCorrectAnswer(QuestionCorrectAnswerDto dto)
//        {
//            var questionCorrectAnswer = new QuestionCorrectAnswer
//            {
//                QuestionId = dto.QuestionId,
//                OptionId = dto.OptionId
//            };

//            _context.QuestionCorrectAnswers.Add(questionCorrectAnswer);
//            await _context.SaveChangesAsync();

//            return Ok(dto);
//        }

//        // PUT: api/QuestionCorrectAnswer/{questionId}/{optionId}
//        [HttpPut("{questionId}/{optionId}")]
//        public async Task<IActionResult> UpdateQuestionCorrectAnswer(int questionId, int optionId, QuestionCorrectAnswerDto dto)
//        {
//            if (questionId != dto.QuestionId || optionId != dto.OptionId)
//            {
//                return BadRequest("QuestionId or OptionId mismatch");
//            }

//            var questionCorrectAnswer = await _context.QuestionCorrectAnswers
//                .FirstOrDefaultAsync(qca => qca.QuestionId == questionId && qca.OptionId == optionId);

//            if (questionCorrectAnswer == null)
//            {
//                return NotFound("QuestionCorrectAnswer not found");
//            }

//            questionCorrectAnswer.QuestionId = dto.QuestionId;
//            questionCorrectAnswer.OptionId = dto.OptionId;

//            _context.Entry(questionCorrectAnswer).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!QuestionCorrectAnswerExists(dto.QuestionId, dto.OptionId))
//                {
//                    return NotFound("QuestionCorrectAnswer not found");
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // DELETE: api/QuestionCorrectAnswer/{questionId}/{optionId}
//        [HttpDelete("{questionId}/{optionId}")]
//        public async Task<IActionResult> DeleteQuestionCorrectAnswer(int questionId, int optionId)
//        {
//            var questionCorrectAnswer = await _context.QuestionCorrectAnswers
//                .FirstOrDefaultAsync(qca => qca.QuestionId == questionId && qca.OptionId == optionId);

//            if (questionCorrectAnswer == null)
//            {
//                return NotFound("QuestionCorrectAnswer not found");
//            }

//            _context.QuestionCorrectAnswers.Remove(questionCorrectAnswer);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool QuestionCorrectAnswerExists(int questionId, int optionId)
//        {
//            return _context.QuestionCorrectAnswers.Any(qca => qca.QuestionId == questionId && qca.OptionId == optionId);
//        }
//    }
}
