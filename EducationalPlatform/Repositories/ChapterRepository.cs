using EducationalPlatform.Data;
using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationalPlatform.Repositories
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly EduPlatformContext _context;

        public ChapterRepository(EduPlatformContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChapterDto>> GetChaptersAsync()
        {
            return await _context.Chapters.Select(c => new ChapterDto
            {
                Id = c.Id,
                Name = c.Name,
                SubjectId = c.SubjectId
            }).ToListAsync();
        }

        public async Task<ChapterDto> GetChapterByIdAsync(int id)
        {
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter == null)
            {
                return null;
            }

            return new ChapterDto
            {
                Id = chapter.Id,
                Name = chapter.Name,
                SubjectId = chapter.SubjectId
            };
        }

        public async Task<IEnumerable<ChapterDto>> GetChaptersBySubjectIdAsync(int subjectId)
        {
            return await _context.Chapters
                                 .Where(c => c.SubjectId == subjectId)
                                 .Select(c => new ChapterDto
                                 {
                                     Id = c.Id,
                                     Name = c.Name,
                                     SubjectId = c.SubjectId
                                 }).ToListAsync();
        }

        public async Task<ChapterDto> CreateChapterAsync(CreateChapterDto dto)
        {
            var chapter = new Chapter
            {
                Name = dto.Name,
                SubjectId = dto.SubjectId
            };

            _context.Chapters.Add(chapter);
            await _context.SaveChangesAsync();

            return new ChapterDto
            {
                Id = chapter.Id,
                Name = chapter.Name,
                SubjectId = chapter.SubjectId
            };
        }

        public async Task UpdateChapterAsync(int id, CreateChapterDto dto)
        {
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter == null)
            {
                return;
            }

            chapter.Name = dto.Name;
            chapter.SubjectId = dto.SubjectId;

            _context.Entry(chapter).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChapterAsync(int id)
        {
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter != null)
            {
                _context.Chapters.Remove(chapter);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> SubjectExistsAsync(int id)
        {
            return await _context.Subjects.AnyAsync(e => e.Id == id);
        }

        public async Task UploadFileAsync(ChapterFile chapterFile)
        {
            _context.ChapterFiles.Add(chapterFile);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChapterFileDto>> GetFilesByChapterIdAsync(int chapterId)
        {
            return await _context.ChapterFiles
                .Where(cf => cf.ChapterId == chapterId)
                .Select(cf => new ChapterFileDto
                {
                    Id = cf.Id,
                    FileName = cf.FileName,
                    ContentType = cf.ContentType,
                    ChapterId = cf.ChapterId,
                    FileContent = cf.FileContent
                }).ToListAsync();
        }
    }
}
