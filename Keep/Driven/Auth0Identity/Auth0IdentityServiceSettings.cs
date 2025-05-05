namespace Keep.Driven.Auth0Identity;

public record Auth0IdentityServiceSettings
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string OpenIdUrl { get; init; }
}