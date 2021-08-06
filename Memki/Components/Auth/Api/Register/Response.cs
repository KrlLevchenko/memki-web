using JetBrains.Annotations;

namespace Memki.Components.Auth.Api.Register
{
    [PublicAPI]
    public class Response
    {
        public UserInfoDto? UserInfo { get; init; }
    }
}