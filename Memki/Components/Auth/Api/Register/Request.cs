using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Memki.Components.Auth.Api.Register
{
    public class Request: IRequest<Response>
    {
        [FromBody] public UserDto UserDto { get; set; } = null!;
    }
}