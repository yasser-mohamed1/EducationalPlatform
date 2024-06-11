using EducationalPlatform.DTO;
using EducationalPlatform.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterFilesController : ControllerBase
    {
        private readonly IChapterFileService _chapterFileService;

        public ChapterFilesController(IChapterFileService chapterFileService)
        {
            _chapterFileService = chapterFileService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChapterFileDto>>> GetChapterFiles()
        {
            var chapterFiles = await _chapterFileService.GetChapterFilesAsync();
            return Ok(chapterFiles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChapterFileDto>> GetChapterFile(int id)
        {
            var chapterFile = await _chapterFileService.GetChapterFileByIdAsync(id);
            if (chapterFile == null)
            {
                return NotFound($"No Chapter File was found with this id : {id}");
            }
            return Ok(chapterFile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutChapterFile(int id, [FromForm] IFormFile file)
        {
            var chapterFileExists = await _chapterFileService.ChapterFileExistsAsync(id);
            if (!chapterFileExists)
            {
                return NotFound($"No Chapter File was found with this id: {id}");
            }

            await _chapterFileService.UpdateChapterFileAsync(id, file);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChapterFile(int id)
        {
            var chapterFileExists = await _chapterFileService.ChapterFileExistsAsync(id);
            if (!chapterFileExists)
            {
                return NotFound($"No Chapter File was found with this id : {id}");
            }

            await _chapterFileService.DeleteChapterFileAsync(id);
            return NoContent();
        }

    }
}
