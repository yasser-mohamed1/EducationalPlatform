using EducationalPlatform.Data;
using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationalPlatform.Repositories
{
    public class ChapterFileRepository : IChapterFileRepository
    {
        private readonly EduPlatformContext _context;

        public ChapterFileRepository(EduPlatformContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChapterFileDto>> GetChapterFilesAsync()
        {
            return await _context.ChapterFiles
                .Select(cf => new ChapterFileDto
                {
                    Id = cf.Id,
                    ChapterId = cf.ChapterId,
                    FileName = cf.FileName,
                    ContentType = cf.ContentType,
                    FileContent = cf.FileContent
                }).ToListAsync();
        }

        public async Task<ChapterFileDto> GetChapterFileByIdAsync(int id)
        {
            var chapterFile = await _context.ChapterFiles.FindAsync(id);
            if (chapterFile == null)
            {
                return null;
            }

            return new ChapterFileDto
            {
                Id = chapterFile.Id,
                ChapterId = chapterFile.ChapterId,
                FileName = chapterFile.FileName,
                ContentType = chapterFile.ContentType,
                FileContent = chapterFile.FileContent
            };
        }

        public async Task UpdateChapterFileAsync(int id, ChapterFileDto chapterFileDto)
        {
            var chapterFile = await _context.ChapterFiles.FindAsync(id);
            if (chapterFile == null)
            {
                return;
            }

            chapterFile.FileName = chapterFileDto.FileName;
            chapterFile.ContentType = chapterFileDto.ContentType;
            chapterFile.FileContent = chapterFileDto.FileContent;

            _context.Entry(chapterFile).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChapterFileAsync(int id)
        {
            var chapterFile = await _context.ChapterFiles.FindAsync(id);
            if (chapterFile == null)
            {
                return;
            }

            _context.ChapterFiles.Remove(chapterFile);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ChapterFileExistsAsync(int id)
        {
            return await _context.ChapterFiles.AnyAsync(e => e.Id == id);
        }
    }
}
