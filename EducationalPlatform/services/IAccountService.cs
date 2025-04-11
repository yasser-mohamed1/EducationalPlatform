using EducationalPlatform.DTO;

namespace EducationalPlatform.Services
{
    public interface IAccountService
    {
        Task<LoginResponseDto> LoginAsync(LoginUserDto userDto);
    }
}
