using JetBrains.Annotations;

namespace Memki.Components.Auth.Api.Register
{
    [PublicAPI]
    public class UserDto
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
        
        public string Name { get; set; } = null!;
    }
}