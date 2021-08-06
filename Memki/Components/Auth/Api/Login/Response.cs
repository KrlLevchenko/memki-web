using JetBrains.Annotations;

namespace Memki.Components.Auth.Api.Login
{
    [PublicAPI]
    public class Response
    {
        public string? Token { get; init; }
    }
}