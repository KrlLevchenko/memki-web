using JetBrains.Annotations;

namespace Memki.Components.Auth.Api.Register
{
    [PublicAPI]
    public class Response
    {
        public string? Token { get; init; } 
    }
}