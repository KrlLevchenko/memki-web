using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using MediatR;
using Memki.Core.Auth;
using Memki.Model;
using Memki.Model.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Memki.Api.Auth.Login
{
    public class Handler: IRequestHandler<Request, Response>
    {
        private readonly Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Handler(Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<Response> Handle(Request request, CancellationToken ct)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Credentials.Email, ct);
            if (user != null)
            {
                var passwordHasher = new PasswordHasher<User>();
                var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Credentials.Password);
                if (result != PasswordVerificationResult.Failed)
                {
                    var claimIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    claimIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
                    claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

                    var jwtToken = JwtTokenGenerator.GenerateToken(claimIdentity);
                    _httpContextAccessor.HttpContext?.Response.Cookies.Append("Token", jwtToken, new CookieOptions
                    {
                        Secure = true,
                        HttpOnly = true
                    });
                    
                    return Response.Success(jwtToken);
                }
            }

            return Response.Fail();
        }
    }
}