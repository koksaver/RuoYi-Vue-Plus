using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RuoYi.Common.Security
{
    public static class JwtAuthExtensions
    {
        public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var secret = configuration["Jwt:Secret"] ?? "RuoYi-Net-Secret-Key-2024-Very-Long-And-Secure";
            var issuer = configuration["Jwt:Issuer"] ?? "RuoYi.NET";
            var audience = configuration["Jwt:Audience"] ?? "RuoYi.App";
            var expireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"] ?? "1440");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization();

            // Register JwtService as singleton
            services.AddSingleton(new JwtService(secret, issuer, audience, expireMinutes));
            services.AddSingleton<PermissionHandler>();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}