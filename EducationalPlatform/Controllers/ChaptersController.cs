using EducationalPlatform.DTO;
using EducationalPlatform.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChaptersController : ControllerBase
    {
        private readonly IChapterService _chapterService;

        public ChaptersController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChapterDto>>> GetChapters()
        {
            var chapters = await _chapterService.GetChaptersAsync();
            return Ok(chapters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChapterDto>> GetChapter(int id)
        {
            var chapter = await _chapterService.GetChapterByIdAsync(id);

            if (chapter == null)
            {
                return NotFound($"No Chapter was found with this id: {id}");
            }

            return Ok(chapter);
        }

        [HttpPost]
        public async Task<ActionResult<ChapterDto>> PostChapter(CreateChapterDto createChapterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var chapter = await _chapterService.CreateChapterAsync(createChapterDto);
                return Ok(chapter);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutChapter(int id, CreateChapterDto updateChapterDto)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _chapterService.UpdateChapterAsync(id, updateChapterDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChapter(int id)
        {
            await _chapterService.DeleteChapterAsync(id);
            return NoContent();
        }

        [HttpGet("BySubject/{subjectId}")]
        public async Task<ActionResult<IEnumerable<ChapterDto>>> GetChaptersBySubject(int subjectId)
        {
            var chapters = await _chapterService.GetChaptersBySubjectIdAsync(subjectId);

            if (chapters == null || !chapters.Any())
            {
                return NotFound($"No Chapters were found with this Subject id: {subjectId}");
            }

            return Ok(chapters);
        }


        [HttpPost("{id}/upload")]
        public async Task<IActionResult> UploadFile(int id, [FromForm] IFormFile file)
        {
            var chapterExists = await _chapterService.GetChapterByIdAsync(id) != null;
            if (!chapterExists)
            {
                return NotFound($"No Chapter was found with this id : {id}");
            }

            await _chapterService.UploadFileAsync(id, file);
            return Ok(new { message = "File uploaded successfully" });
        }

        [HttpGet("{id}/files")]
        public async Task<IActionResult> GetFiles(int id)
        {
            var chapterFiles = await _chapterService.GetFilesByChapterIdAsync(id);
            if (chapterFiles == null || !chapterFiles.Any())
            {
                return NotFound($"No Chapter Files were found with this Chapter id : {id}");
            }

            return Ok(chapterFiles);
        }
    }
}
