using System.Net.Http.Json;
using System.Text.Json;
using ECommerceBackend.Utils.Jwt;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ECommerceBackend.Utils.Auth;

public class BackgroundAccessTokenFetcher : BackgroundService
{
    private readonly ILogger<BackgroundAccessTokenFetcher> _logger;
    private readonly OidcConfig _jwtConfig;
    private readonly HttpClient _httpClient;
    private readonly InternalJwtTokenContainer _jwtTokenContainer;

    public BackgroundAccessTokenFetcher(ILogger<BackgroundAccessTokenFetcher> logger,
        IOptions<OidcConfig> jwtConfig,
        HttpClient httpClient,
        InternalJwtTokenContainer jwtTokenContainer)
    {
        _logger = logger;
        _jwtConfig = jwtConfig.Value;
        _httpClient = httpClient;
        _jwtTokenContainer = jwtTokenContainer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ReceiveToken(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            if (_jwtTokenContainer.ExpirationDate < DateTime.Now)
            {
                bool success = await ReceiveToken(stoppingToken);
                if (!success)
                    await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
            else
            {
                TimeSpan expiresIn = _jwtTokenContainer.ExpirationDate - DateTime.Now;
                _logger.LogDebug("Waiting for next token fetch for {} seconds", expiresIn.TotalSeconds);
                await Task.Delay(expiresIn, stoppingToken);
            }
        }
    }

    private async Task<bool> ReceiveToken(CancellationToken cancellationToken)
    {
        _logger.LogTrace("Entered: {}", nameof(ReceiveToken));
        string url = $"{Path.Combine(_jwtConfig.Authority, "protocol/openid-connect/token")}";
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
        {
            new("client_id", _jwtConfig.ClientId),
            new("client_secret", _jwtConfig.SecretClientId),
            new("grant_type", "client_credentials"),
            new("audience", _jwtConfig.Audience),
        });

        try
        {
            HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to fetch access token! Status code: {StatusCode}", response.StatusCode);
                return false;
            }
            response.EnsureSuccessStatusCode();
            JsonElement contentJson = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken);
            string? accessToken = contentJson.GetProperty("access_token").GetString();
            if (accessToken == null)
                return false;
            int expiresIn = contentJson.GetProperty("expires_in").GetInt32();
            DateTime expiresAt = DateTime.Now.AddSeconds(expiresIn - 5);
            _jwtTokenContainer.AccessToken = accessToken;
            _jwtTokenContainer.ExpirationDate = expiresAt;
            _logger.LogDebug("Received access token for {} seconds", expiresIn);
            return true;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError("HttpRequestException {@Exception} in {Location}.", ex, nameof(ReceiveToken));
        }
        return false;
    }
}