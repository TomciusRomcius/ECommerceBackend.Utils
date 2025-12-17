using System.Text;
using ECommerceBackend.Utils.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ECommerceBackend.Utils.Auth;

public static class JwtClaimsReader
{
    public static JwtClaims? Read(HttpContext httpContext)
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
            return new JwtClaims
            {
                Issuer = issuer,
                Actor = actor,
            };
        }

        return null;
    }
}

public static class AuthSetup
{
    public static IServiceCollection AddApplicationAuth(this IServiceCollection sc, WebApplicationBuilder builder)
    {
        IConfiguration jwtSection = builder.Configuration.GetSection("Jwt");
        sc.AddOptions<JwtAuthConfiguration>()
            .Bind(jwtSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // AddOptions validates JsonAuthConfiguration, whether it is null
        JwtAuthConfiguration jwtConfig = jwtSection.Get<JwtAuthConfiguration>()!;

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SigningKey));
                options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(1);
                options.TokenValidationParameters.IssuerSigningKey = key;
                options.TokenValidationParameters.ValidateIssuerSigningKey = true;
                options.TokenValidationParameters.ValidateIssuer = true;
                options.TokenValidationParameters.ValidIssuers = [jwtConfig.Issuer];
                options.TokenValidationParameters.ValidateAudience = false;
                options.TokenValidationParameters.ValidateLifetime = true;
            });

        return sc;
    }

    public static IApplicationBuilder UseApplicationAuth(this IApplicationBuilder ab)
    {
        ab.UseAuthentication();
        ab.UseAuthorization();
        return ab;
    }
}