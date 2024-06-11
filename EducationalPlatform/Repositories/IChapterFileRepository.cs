using EducationalPlatform.DTO;
using EducationalPlatform.Entities;

namespace EducationalPlatform.Repositories
{
    public interface IChapterFileRepository
    {
        Task<IEnumerable<ChapterFileDto>> GetChapterFilesAsync();
        Task<ChapterFileDto> GetChapterFileByIdAsync(int id);
        Task UpdateChapterFileAsync(int id, ChapterFileDto chapterFileDto);
        Task DeleteChapterFileAsync(int id);
        Task<bool> ChapterFileExistsAsync(int id);
    }
}
