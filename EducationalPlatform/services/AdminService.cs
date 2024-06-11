using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using EducationalPlatform.Repositories;

namespace EducationalPlatform.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<string> RegisterAdminAsync(RegisterAdminDto adminDto)
        {
            var user = new ApplicationUser
            {
                UserName = adminDto.Username,
                Email = adminDto.Email,
                PhoneNumber = adminDto.Phone
            };

            var result = await _adminRepository.CreateUserAsync(user, adminDto.Password);

            if (!result.Succeeded)
            {
                return string.Join(", ", result.Errors.Select(e => e.Description));
            }

            await _adminRepository.AddUserToRoleAsync(user, "Admin");

            return "Admin Registration Success";
        }

        public async Task<string> CreateRoleAsync(string role)
        {
            var result = await _adminRepository.CreateRoleAsync(role);

            if (!result.Succeeded)
            {
                return string.Join(", ", result.Errors.Select(e => e.Description));
            }

            return "Role Created Successfully";
        }
    }
}
