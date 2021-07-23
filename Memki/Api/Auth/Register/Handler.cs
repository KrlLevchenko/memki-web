using System;
using System.Threading;
using System.Threading.Tasks;
using Dodo.Primitives;
using LinqToDB;
using MediatR;
using Memki.Api.Auth.Login;
using Memki.Model;
using Memki.Model.Auth;
using Microsoft.AspNetCore.Identity;

namespace Memki.Api.Auth.Register
{
    public class Handler: IRequestHandler<Request,Response>
    {
        private readonly Context _context;
        private readonly IMediator _mediator;
        

        public Handler(Context context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Response> Handle(Request request, CancellationToken ct)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.UserDto.Email, ct);
            if (user != null)
            {
                return Response.UserAlreadyExist();
            }
            
            await CreateUser(request, ct);
            var token = await GetToken(request, ct);
            
            return Response.Success(token);
        }

        private async Task CreateUser(Request request, CancellationToken ct)
        {
            var passwordHasher = new PasswordHasher<User>();

            var user = new User(Uuid.NewMySqlOptimized(), request.UserDto.Name, request.UserDto.Email, null);
            user.SetPasswordHash(passwordHasher.HashPassword(user, request.UserDto.Password));
            await _context.InsertAsync(user, token: ct);
        }

        private async Task<string> GetToken(Request request, CancellationToken ct)
        {
            var loginRequest = new Login.Request
            {
                Credentials = new Credentials
                {
                    Email = request.UserDto.Email,
                    Password = request.UserDto.Password
                }
            };
            var loginResponse = await _mediator.Send(loginRequest, ct);
            if (!loginResponse.Ok)
                throw new Exception("Unexpected error - cannot get token for new user");
            return loginResponse.Token;
        }
    }
}