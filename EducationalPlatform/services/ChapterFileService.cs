using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using EducationalPlatform.Repositories;

namespace EducationalPlatform.Services
{
    public class ChapterFileService : IChapterFileService
    {
        private readonly IChapterFileRepository _chapterFileRepository;

        public ChapterFileService(IChapterFileRepository chapterFileRepository)
        {
            _chapterFileRepository = chapterFileRepository;
        }

        public async Task<IEnumerable<ChapterFileDto>> GetChapterFilesAsync()
        {
            return await _chapterFileRepository.GetChapterFilesAsync();
        }

        public async Task<ChapterFileDto> GetChapterFileByIdAsync(int id)
        {
            return await _chapterFileRepository.GetChapterFileByIdAsync(id);
        }

        public async Task UpdateChapterFileAsync(int id, IFormFile file)
        {
            var chapterFileDto = new ChapterFileDto
            {
                FileName = file.FileName,
                ContentType = file.ContentType
            };

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                chapterFileDto.FileContent = memoryStream.ToArray();
            }

            await _chapterFileRepository.UpdateChapterFileAsync(id, chapterFileDto);
        }

        public async Task DeleteChapterFileAsync(int id)
        {
            await _chapterFileRepository.DeleteChapterFileAsync(id);
        }

        public async Task<bool> ChapterFileExistsAsync(int id)
        {
            return await _chapterFileRepository.ChapterFileExistsAsync(id);
        }
    }
}
