using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Memki.AppStart
{
    public static class AuthConfig
    {
        public const string Issuer = "Memki";
        public const string Audience = "MemkiApiClients";
        public static readonly SymmetricSecurityKey Key = 
            new(Encoding.ASCII.GetBytes("mysupersecret_secretkey!123")); // where to store secret???

        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Issuer,
            
                        ValidateAudience = true,
                        ValidAudience = Audience,
            
                        ValidateLifetime = true,
            
                        IssuerSigningKey = Key,
                        ValidateIssuerSigningKey = true,
                    };
            });
            
            return services;
        }
    }
}