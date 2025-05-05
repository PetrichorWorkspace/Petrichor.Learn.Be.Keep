using Shared.Core.Driving.Authentication.ApiKey;

namespace Keep.Driving.ForAuth0;

public static class SecurityForAuth0
{
    public const string Policy = "SecurityForAuth0Policy";
    public const string Scheme = "SecurityForAuth0Scheme";

    private static void AddAuth0ApiKeyAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication()
            .AddApiKey(Scheme);
    }

    private static void AddAuth0ApiKeyAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(Policy, policy =>
            {
                policy.AuthenticationSchemes.Add(Scheme);
                policy.RequireAuthenticatedUser();
            });
    }
    
    public static void AddSecurityForAuth0(this IServiceCollection services)
    {
        services.AddAuth0ApiKeyAuthentication();
        services.AddAuth0ApiKeyAuthorization();
    }
}