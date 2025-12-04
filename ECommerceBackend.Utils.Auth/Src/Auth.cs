using System.Security.Claims;
using System.Text;
using ECommerceBackend.Utils.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ECommerceBackend.Utils.Auth;

public static class JwtUserReader
{
    public static JwtClaims? ReadJwt(HttpContext httpContext)
    {
        string? issuer = httpContext.User.Claims
            .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Iss)
            ?.Value;
        string? actor = httpContext.User.Claims
            .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Acr)
            ?.Value;
        if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(actor))
        {
            return null;
        }

        if (actor == ActorTypes.Client)
        {
            return new ClientJwtClaims
            {
                Issuer = issuer,
                Actor = actor,
                UserId = httpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.NameId).Value,
                Email = httpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Email).Value
            };
        }
        else if (actor == ActorTypes.Microservice)
        {
            return new MicroserviceJwtClaims
            {
                Issuer = issuer,
                Actor = actor,
                Permissions = httpContext.User.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)
                    .ToArray()
            };
        }

        return null;
    }
}

public static class AuthSetup
{
    public static IServiceCollection AddApplicationAuth(this IServiceCollection sc, JwtAuthConfiguration config)
    {
        sc.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.SigningKey));
                options.Authority = config.Issuer;
                options.Audience = config.Audience;
                options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(1);

                options.TokenValidationParameters.ValidateAudience = true;
                options.TokenValidationParameters.ValidateIssuer = true;
                options.TokenValidationParameters.ValidateIssuerSigningKey = true;
                options.TokenValidationParameters.ValidateLifetime = true;
            });
        return sc;
    }

    public static IApplicationBuilder UseApplicationAuth(this IApplicationBuilder ab, JwtAuthConfiguration config)
    {
        ab.UseAuthentication();
        ab.UseAuthorization();
        return ab;
    }
}