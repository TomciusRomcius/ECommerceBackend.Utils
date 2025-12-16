namespace ECommerceBackend.Utils.Jwt;

public class JwtAuthConfiguration
{
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required string SigningKey { get; set; }
    public required int LifetimeMinutes { get; set; }
    public required int ClockSkewMinutes { get; set; }
}

public static class ActorTypes
{
    public static string Client { get; } = "client";
    public static string Microservice { get; } = "microservice";
}

public class JwtClaims
{
    public required string Issuer { get; init; }
    public required string Actor { get; init; }
}

public class ClientJwtClaims : JwtClaims
{
    public required string UserId { get; init; }
    public required string Email { get; init; }
}