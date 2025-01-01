using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Shared.Core.Driving.Security.ApiKey;

namespace Keep.Driving.Common.Security;

public static class SecurityForAuth0
{
    public const string Policy = "SecurityForAuth0Policy";
    public const string Scheme = "SecurityForAuth0Scheme";

    public static AuthenticationBuilder AddAuth0ApiKeyAuthentication(this AuthenticationBuilder builder)
    {
        builder.AddApiKey(Scheme);
        return builder;
    }

    public static AuthorizationBuilder AddAuth0ApiKeyAuthorization(this AuthorizationBuilder builder)
    {
        builder.AddPolicy(Policy, policy =>
            {
                policy.AuthenticationSchemes.Add(Scheme);
                policy.RequireAuthenticatedUser();
            });
        
        return builder;
    }
}