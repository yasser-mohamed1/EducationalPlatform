using EducationalPlatform.Entities;

namespace EducationalPlatform.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> FindByNameAsync(string userName);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
    }
}
