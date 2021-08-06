using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Memki.Components.Auth.Api.Login
{
    [PublicAPI]
    public class Request : IRequest<Response>
    {
        [FromBody] public CredentialsDto Credentials { get; set; } = null!;
    }
}