namespace Memki.Api.Auth.Register
{
    public class UserDto
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
        
        public string Name { get; set; } = null!;
    }
}