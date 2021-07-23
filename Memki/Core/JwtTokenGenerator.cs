using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Memki.AppStart;
using Microsoft.IdentityModel.Tokens;

namespace Memki.Core
{
    public static class JwtTokenGenerator
    {
        public static string GenerateToken(ClaimsIdentity claimIdentity)
        {
            var jwt = new JwtSecurityToken(
                issuer: AuthConfig.Issuer,
                audience: AuthConfig.Audience,
                notBefore: DateTime.UtcNow,
                claims: claimIdentity.Claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(24)),
                signingCredentials: new SigningCredentials(AuthConfig.Key, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}