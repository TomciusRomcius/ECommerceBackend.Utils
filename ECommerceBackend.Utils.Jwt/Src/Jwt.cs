namespace ECommerceBackend.Utils.Jwt;

public class JwtAuthConfiguration
{
    public required string Issuer { get; set; }
    public required string SigningKey { get; set; }
    public required int LifetimeMinutes { get; set; }
    public required int ClockSkewMinutes { get; set; }
}

public static class RoleTypes
{
    public const string Client = "client";
    public const string Admin = "admin";
}

public class JwtClaims
{
    public required string Issuer { get; init; }
    public required string Roles { get; init; }
}

public class ClientJwtClaims : JwtClaims
{
    public required string UserId { get; init; }
    public required string Email { get; init; }
}
