using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using EducationalPlatform.Repositories;

namespace EducationalPlatform.Services
{
    public class ChapterService : IChapterService
    {
        private readonly IChapterRepository _chapterRepository;

        public ChapterService(IChapterRepository chapterRepository)
        {
            _chapterRepository = chapterRepository;
        }

        public async Task<IEnumerable<ChapterDto>> GetChaptersAsync()
        {
            return await _chapterRepository.GetChaptersAsync();
        }

        public async Task<ChapterDto> GetChapterByIdAsync(int id)
        {
            return await _chapterRepository.GetChapterByIdAsync(id);
        }

        public async Task<IEnumerable<ChapterDto>> GetChaptersBySubjectIdAsync(int subjectId)
        {
            return await _chapterRepository.GetChaptersBySubjectIdAsync(subjectId);
        }

        public async Task<ChapterDto> CreateChapterAsync(CreateChapterDto dto)
        {
            if (!await _chapterRepository.SubjectExistsAsync(dto.SubjectId))
            {
                throw new KeyNotFoundException($"No Subject was found with this id: {dto.SubjectId}");
            }

            return await _chapterRepository.CreateChapterAsync(dto);
        }

        public async Task UpdateChapterAsync(int id, CreateChapterDto dto)
        {
            if (!await _chapterRepository.SubjectExistsAsync(dto.SubjectId))
            {
                throw new KeyNotFoundException($"No Subject was found with this id: {dto.SubjectId}");
            }

            await _chapterRepository.UpdateChapterAsync(id, dto);
        }

        public async Task DeleteChapterAsync(int id)
        {
            await _chapterRepository.DeleteChapterAsync(id);
        }


        public async Task UploadFileAsync(int chapterId, IFormFile file)
        {
            ChapterDto chapter = await _chapterRepository.GetChapterByIdAsync(chapterId);
            if(chapter == null) 
            {
                throw new KeyNotFoundException("No Chapter was found with this id");
            }
            if (file != null && file.Length > 0)
            {
                var chapterFile = new ChapterFile
                {
                    ChapterId = chapterId,
                    FileName = file.FileName,
                    ContentType = file.ContentType
                };

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    chapterFile.FileContent = memoryStream.ToArray();
                }

                await _chapterRepository.UploadFileAsync(chapterFile);
            }
        }

        public async Task<IEnumerable<ChapterFileDto>> GetFilesByChapterIdAsync(int chapterId)
        {
            return await _chapterRepository.GetFilesByChapterIdAsync(chapterId);
        }
    }
}
