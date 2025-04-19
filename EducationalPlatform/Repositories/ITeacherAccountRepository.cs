using EducationalPlatform.Entities;
using Microsoft.AspNetCore.Identity;

namespace EducationalPlatform.Repositories
{
    public interface ITeacherAccountRepository
    {
        Task<bool> IsUsernameTakenAsync(string username);
        Task<bool> IsEmailTakenAsync(string email);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string role);
        Task AddTeacherAsync(Teacher teacher);
        Task SaveChangesAsync();
    }
}
