using EducationalPlatform.Entities;
using Microsoft.AspNetCore.Identity;

namespace EducationalPlatform.Repositories
{
    public interface IAdminRepository
    {
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string role);
        Task<IdentityResult> CreateRoleAsync(string role);
    }
}
