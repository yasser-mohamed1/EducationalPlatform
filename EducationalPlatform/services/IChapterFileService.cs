using EducationalPlatform.DTO;

namespace EducationalPlatform.Services
{
    public interface IChapterFileService
    {
        Task<IEnumerable<ChapterFileDto>> GetChapterFilesAsync();
        Task<ChapterFileDto> GetChapterFileByIdAsync(int id);
        Task UpdateChapterFileAsync(int id, IFormFile file);
        Task DeleteChapterFileAsync(int id);
        Task<bool> ChapterFileExistsAsync(int id);
    }
}
