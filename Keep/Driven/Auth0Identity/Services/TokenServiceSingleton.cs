using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Keep.Driven.Auth0Identity.Services;

public interface ITokenServiceSingleton
{
    Task<TokenValidationParameters> GetValidationParametersAsync();
}

public class TokenServiceSingleton : ITokenServiceSingleton
{
    private readonly IConfigurationManager<OpenIdConnectConfiguration> _configManager;
    private readonly Auth0IdentityServiceSettings _settings;

    public TokenServiceSingleton(IOptions<Auth0IdentityServiceSettings> options)
    {
        _settings = options.Value;
        _configManager = new ConfigurationManager<OpenIdConnectConfiguration>(_settings.OpenIdUrl, new OpenIdConnectConfigurationRetriever());
    }

    public async Task<TokenValidationParameters> GetValidationParametersAsync()
    {
        var configuration = await _configManager.GetConfigurationAsync(CancellationToken.None);

        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKeys = configuration.SigningKeys,
            ValidateIssuer = true,
            ValidIssuer = _settings.Issuer,
            ValidateAudience = true,
            ValidAudience = _settings.Audience,
            ValidateLifetime = true,
        };
    }
}