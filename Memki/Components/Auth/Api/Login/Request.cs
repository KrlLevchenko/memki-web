using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Memki.Components.Auth.Api.Login
{
    public class Request : IRequest<Response>
    {
        [FromBody] public Credentials Credentials { get; set; }
    }
}