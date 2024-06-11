using EducationalPlatform.DTO;
using EducationalPlatform.Entities;

namespace EducationalPlatform.Repositories
{
    public interface IChapterRepository
    {
        Task<IEnumerable<ChapterDto>> GetChaptersAsync();
        Task<ChapterDto> GetChapterByIdAsync(int id);
        Task<IEnumerable<ChapterDto>> GetChaptersBySubjectIdAsync(int subjectId);
        Task<ChapterDto> CreateChapterAsync(CreateChapterDto dto);
        Task UpdateChapterAsync(int id, CreateChapterDto dto);
        Task DeleteChapterAsync(int id);
        Task<bool> SubjectExistsAsync(int id);
        Task UploadFileAsync(ChapterFile chapterFile);
        Task<IEnumerable<ChapterFileDto>> GetFilesByChapterIdAsync(int chapterId);
    }
}
