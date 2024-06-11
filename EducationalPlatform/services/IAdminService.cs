using EducationalPlatform.DTO;

namespace EducationalPlatform.Services
{
    public interface IAdminService
    {
        Task<string> RegisterAdminAsync(RegisterAdminDto adminDto);
        Task<string> CreateRoleAsync(string role);
    }
}
