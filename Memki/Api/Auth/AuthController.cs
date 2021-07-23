using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Memki.Api.Auth
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController: Controller
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public Task<Login.Response> Login(Login.Request request, CancellationToken ct) => _mediator.Send(request, ct);
        
        [HttpPost("register")]
        public Task<Register.Response> Register(Register.Request request, CancellationToken ct) => _mediator.Send(request, ct);
    }
}