using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Memki.Api.Auth.Login
{
    public class Request : IRequest<Response>
    {
        [FromBody] public Credentials Credentials { get; set; }
    }
}