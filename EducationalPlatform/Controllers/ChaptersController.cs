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
    public class ChaptersController : ControllerBase
    {
        private readonly EduPlatformContext _context;

        public ChaptersController(EduPlatformContext context)
        {
            _context = context;
        }

        // GET: api/Chapters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChapterDto>>> GetChapters()
        {
            var chapters = await _context.Chapters.ToListAsync();
            var chapterDtos = chapters.Select(c => new ChapterDto
            {
                Id = c.Id,
                Name = c.Name,
                SubjectId = c.SubjectId
            }).ToList();
            return Ok(chapterDtos);
        }

        // GET: api/Chapters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChapterDto>> GetChapter(int id)
        {
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter == null)
            {
                return NotFound($"No Chapter was found with this id : {id}");
            }
            var chapterDto = new ChapterDto
            {
                Id = chapter.Id,
                Name = chapter.Name,
                SubjectId = chapter.SubjectId
            };
            return Ok(chapterDto);
        }

        // POST: api/Chapters
        [HttpPost]
        public async Task<ActionResult<ChapterDto>> PostChapter(CreateChapterDto createChapterDto)
        {
            Subject? subject = await _context.Subjects.FindAsync(createChapterDto.SubjectId);
            if(subject == null)
            {
                return NotFound($"No Subject was found with this id : {createChapterDto.SubjectId}");
            }
            var chapter = new Chapter
            {
                Name = createChapterDto.Name,
                SubjectId = createChapterDto.SubjectId
            };

            _context.Chapters.Add(chapter);
            await _context.SaveChangesAsync();

            var chapterDto = new ChapterDto
            {
                Id = chapter.Id,
                Name = chapter.Name,
                SubjectId = chapter.SubjectId
            };

            return Ok(chapterDto);
        }

        // PUT: api/Chapters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChapter(int id, CreateChapterDto updateChapterDto)
        {
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter == null)
            {
                return NotFound($"No Chapter was found with this id : {id}");
            }

            chapter.Name = updateChapterDto.Name;
            chapter.SubjectId = updateChapterDto.SubjectId;

            _context.Entry(chapter).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChapterExists(id))
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

        // DELETE: api/Chapters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChapter(int id)
        {
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter == null)
            {
                return NotFound($"No Chapter was found with this id : {id}");
            }

            _context.Chapters.Remove(chapter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChapterExists(int id)
        {
            return _context.Chapters.Any(e => e.Id == id);
        }
        
        // POST: api/Chapters/{id}/upload
        [HttpPost("{id}/upload")]
        public async Task<IActionResult> UploadFile(int id, [FromForm] IFormFile file)
        {
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter == null)
            {
                return NotFound($"No Chapter was found with this id : {id}");
            }

            if (file != null && file.Length > 0)
            {
                var chapterFile = new ChapterFile
                {
                    ChapterId = chapter.Id,
                    FileName = file.FileName,
                    ContentType = file.ContentType
                };

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    chapterFile.FileContent = memoryStream.ToArray();
                }

                _context.ChapterFiles.Add(chapterFile);
                await _context.SaveChangesAsync();

                return Ok(new { message = "File uploaded successfully." });
            }

            return BadRequest(new { message = "Invalid file." });
        }

        // GET: api/Chapters/BySubject/5
        [HttpGet("BySubject/{subjectId}")]
        public async Task<ActionResult<IEnumerable<ChapterDto>>> GetChaptersBySubject(int subjectId)
        {
            var chapters = await _context.Chapters
                                         .Where(c => c.SubjectId == subjectId)
                                         .ToListAsync();

            if (chapters == null || !chapters.Any())
            {
                return NotFound($"No Chapters were found with this Subject id : {subjectId}");
            }

            var chapterDtos = chapters.Select(c => new ChapterDto
            {
                Id = c.Id,
                Name = c.Name,
                SubjectId = c.SubjectId
            }).ToList();

            return Ok(chapterDtos);
        }

        // GET: api/Chapters/{id}/files
        [HttpGet("{id}/files")]
        public async Task<IActionResult> GetFiles(int id)
        {
            var chapterFiles = await _context.ChapterFiles
                .Where(cf => cf.ChapterId == id)
                .Select(cf => new ChapterFileDto()
                {
                    Id = cf.Id,
                    FileName = cf.FileName,
                    ContentType = cf.ContentType,
                    ChapterId = cf.ChapterId,
                    FileContent = cf.FileContent
                })
                .ToListAsync();

            if (!chapterFiles.Any())
            {
                return NotFound($"No Chapter Files were found with this Chapter id : {id}");
            }

            return Ok(chapterFiles);
        }

    }
}
