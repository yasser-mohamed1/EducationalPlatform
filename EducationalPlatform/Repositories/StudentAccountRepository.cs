using EducationalPlatform.Data;
using EducationalPlatform.Entities;
using Microsoft.AspNetCore.Identity;

namespace EducationalPlatform.Repositories
{
    public class StudentAccountRepository : IStudentAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EduPlatformContext _context;

        public StudentAccountRepository(UserManager<ApplicationUser> userManager, EduPlatformContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task AddStudentAsync(Student student)
        {
            await _context.Students.AddAsync(student);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
