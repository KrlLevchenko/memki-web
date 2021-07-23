using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Memki.Api.Auth.Register
{
    public class Request: IRequest<Response>
    {
        [FromBody] public UserDto UserDto { get; set; }

    }
}