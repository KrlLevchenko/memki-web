namespace Memki.Components.Auth.Api.Register
{
    public class UserInfoDto
    {
        public string Name { get; init; } = null!;

        public string Email { get; init; } = null!;

        public string Token { get; init; } = null!;
    }
}