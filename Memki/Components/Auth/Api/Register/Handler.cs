using System;
using System.Threading;
using System.Threading.Tasks;
using Dodo.Primitives;
using JetBrains.Annotations;
using LinqToDB;
using MediatR;
using Memki.Components.Auth.Api.Login;
using Memki.Model;
using Memki.Model.Auth.Entities;
using Memki.Model.Auth.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Memki.Components.Auth.Api.Register
{
    [UsedImplicitly]
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
                throw new UserDuplicateException(user.Id, user.Email);
            }
            
            await CreateUser(request, ct);
            var userInfo = await GetUserInfo(request, ct);
            
            return new Response
            {
                UserInfo = userInfo
            };
        }

        private async Task CreateUser(Request request, CancellationToken ct)
        {
            var passwordHasher = new PasswordHasher<User>();

            var user = new User(Uuid.NewMySqlOptimized(), request.UserDto.Name, request.UserDto.Email, null);
            user.SetPasswordHash(passwordHasher.HashPassword(user, request.UserDto.Password));
            await _context.InsertAsync(user, token: ct);
        }

        private async Task<UserInfoDto> GetUserInfo(Request request, CancellationToken ct)
        {
            var loginRequest = new Login.Request
            {
                Credentials = new CredentialsDto
                {
                    Email = request.UserDto.Email,
                    Password = request.UserDto.Password
                }
            };
            var loginResponse = await _mediator.Send(loginRequest, ct);
            if (loginResponse.UserInfo == null)
                throw new Exception("Unexpected error - cannot get token for new user");
            return new UserInfoDto
            {
                Email = loginResponse.UserInfo.Email,
                Name = loginResponse.UserInfo.Name,
                Token = loginResponse.UserInfo.Token,
            };
        }
    }
}