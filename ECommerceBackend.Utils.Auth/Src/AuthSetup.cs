using ECommerceBackend.Utils.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ECommerceBackend.Utils.Auth.Src;

public static class AuthSetup
{
    public static IServiceCollection AddApplicationAuth(this IServiceCollection sc, WebApplicationBuilder builder)
    {
        IConfiguration jwtSection = builder.Configuration.GetSection("Jwt");
        sc.AddOptions<OidcConfig>()
            .Bind(jwtSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        OidcConfig oidcConfig = jwtSection.Get<OidcConfig>()!;
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = oidcConfig.Authority;
                options.RequireHttpsMetadata = builder.Environment.IsProduction();
                options.Audience = oidcConfig.Audience;
            });

        return sc;
    }

    public static IServiceCollection AddBackgroundJwtRefresher(this IServiceCollection sc)
    {
        sc.AddSingleton<InternalJwtTokenContainer>();
        sc.AddScoped<JwtTokenReader>();
        return sc.AddHostedService<BackgroundAccessTokenFetcher>();
    }

    public static IApplicationBuilder UseApplicationAuth(this IApplicationBuilder ab)
    {
        ab.UseAuthentication();
        ab.UseAuthorization();
        return ab;
    }
}