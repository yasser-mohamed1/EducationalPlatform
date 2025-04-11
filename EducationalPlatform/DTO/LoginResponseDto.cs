namespace EducationalPlatform.DTO
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}