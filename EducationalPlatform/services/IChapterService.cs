using EducationalPlatform.DTO;
using EducationalPlatform.Entities;

namespace EducationalPlatform.Services
{
    public interface IChapterService
    {
        Task<IEnumerable<ChapterDto>> GetChaptersAsync();
        Task<ChapterDto> GetChapterByIdAsync(int id);
        Task<IEnumerable<ChapterDto>> GetChaptersBySubjectIdAsync(int subjectId);
        Task<ChapterDto> CreateChapterAsync(CreateChapterDto dto);
        Task UpdateChapterAsync(int id, CreateChapterDto dto);
        Task DeleteChapterAsync(int id);
        Task UploadFileAsync(int chapterId, IFormFile file);
        Task<IEnumerable<ChapterFileDto>> GetFilesByChapterIdAsync(int chapterId);
    }
}
