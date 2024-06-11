using EducationalPlatform.DTO;

namespace EducationalPlatform.Services
{
    public interface IAccountService
    {
        Task<object> LoginAsync(LoginUserDto userDto);
    }
}
