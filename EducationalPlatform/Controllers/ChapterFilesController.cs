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
    public class ChapterFilesController : ControllerBase
    {
        private readonly EduPlatformContext _context;

        public ChapterFilesController(EduPlatformContext context)
        {
            _context = context;
        }

        // GET: api/ChapterFiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChapterFileDto>>> GetChapterFiles()
        {
            var chapterFiles = await _context.ChapterFiles.ToListAsync();
            var chapterFileDtos = chapterFiles.Select(cf => new ChapterFileDto
            {
                Id = cf.Id,
                ChapterId = cf.ChapterId,
                FileName = cf.FileName,
                ContentType = cf.ContentType,
                FileContent = cf.FileContent
            }).ToList();
            return Ok(chapterFileDtos);
        }

        // GET: api/ChapterFiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChapterFileDto>> GetChapterFile(int id)
        {
            var chapterFile = await _context.ChapterFiles.FindAsync(id);
            if (chapterFile == null)
            {
                return NotFound($"No Chapter File was found with this id : {id}");
            }
            var chapterFileDto = new ChapterFileDto
            {
                Id = chapterFile.Id,
                ChapterId = chapterFile.ChapterId,
                FileName = chapterFile.FileName,
                ContentType = chapterFile.ContentType,
                FileContent = chapterFile.FileContent
            };
            return Ok(chapterFileDto);
        }

        // POST: api/ChapterFiles
        [HttpPost]
        public async Task<ActionResult<ChapterFileDto>> PostChapterFile(CreateChapterFileDto createChapterFileDto)
        {

            Chapter? chapter = await _context.Chapters.FindAsync(createChapterFileDto.ChapterId);
            if (chapter == null)
            {
                return NotFound($"No Chapter was found with this id : {createChapterFileDto.ChapterId}");
            }

            var chapterFile = new ChapterFile
            {
                ChapterId = createChapterFileDto.ChapterId,
                FileName = createChapterFileDto.FileName,
                ContentType = createChapterFileDto.ContentType,
                FileContent = createChapterFileDto.FileContent
            };

            _context.ChapterFiles.Add(chapterFile);
            await _context.SaveChangesAsync();

            var chapterFileDto = new ChapterFileDto
            {
                Id = chapterFile.Id,
                ChapterId = chapterFile.ChapterId,
                FileName = chapterFile.FileName,
                ContentType = chapterFile.ContentType,
                FileContent = chapterFile.FileContent
            };

            return Ok(chapterFileDto);
        }

        // PUT: api/ChapterFiles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChapterFile(int id, UpdateChapterFileDto updateChapterFileDto)
        {
            var chapterFile = await _context.ChapterFiles.FindAsync(id);
            if (chapterFile == null)
            {
                return NotFound($"No Chapter File was found with this id : {id}");
            }

            chapterFile.FileName = updateChapterFileDto.FileName;
            chapterFile.ContentType = updateChapterFileDto.ContentType;
            chapterFile.FileContent = updateChapterFileDto.FileContent;

            _context.Entry(chapterFile).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChapterFileExists(id))
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

        // DELETE: api/ChapterFiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChapterFile(int id)
        {
            var chapterFile = await _context.ChapterFiles.FindAsync(id);
            if (chapterFile == null)
            {
                return NotFound($"No Chapter File was found with this id : {id}");
            }

            _context.ChapterFiles.Remove(chapterFile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChapterFileExists(int id)
        {
            return _context.ChapterFiles.Any(e => e.Id == id);
        }
    }
}