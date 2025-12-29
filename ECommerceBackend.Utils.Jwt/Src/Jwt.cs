using System.ComponentModel.DataAnnotations;

namespace ECommerceBackend.Utils.Jwt;

public static class RoleTypes
{
    public const string Admin = "ecommerce-admin";
}

/// <summary>
/// Should not be injected in any services. For reading the access token JwtTokenContainerReader should be used.
/// </summary>
public class OidcConfig
{
    [Required]
    public required string ClientId { get; init; }
    [Required]
    public required string SecretClientId { get; init; }
    [Required]
    public required string Audience { get; init; }
    [Required]
    public required string Authority { get; init; }
}

public class InternalJwtTokenContainer()
{
    public string AccessToken { get; set; } = "";
    public DateTime ExpirationDate { get; set; }
}

public class JwtTokenReader
{
    private readonly InternalJwtTokenContainer _jwtTokenContainer;

    public JwtTokenReader(InternalJwtTokenContainer jwtTokenContainer)
    {
        _jwtTokenContainer = jwtTokenContainer;
    }

    public string AccessToken => _jwtTokenContainer.AccessToken;
    public DateTime ExpirationDate => _jwtTokenContainer.ExpirationDate;
}