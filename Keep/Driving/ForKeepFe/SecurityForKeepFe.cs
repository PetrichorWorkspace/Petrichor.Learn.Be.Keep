using Keep.Driven.Auth0Identity.Services;

namespace Keep.Driving.ForKeepFe;

public static class SecurityForKeepFe
{
    public const string Policy = "SecurityForKeepFePolicy";
    public const string Scheme = "SecurityForKeepFeScheme";
    
    private static void AddKeepFeJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication()
            .AddJwtBearer(Scheme, options =>
            {
                var tokenService = services.BuildServiceProvider().GetRequiredService<ITokenServiceSingleton>();
                options.TokenValidationParameters = tokenService.GetValidationParametersAsync().Result;
            });
    }

    private static void AddKeepFeJwtAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder().AddPolicy(Policy, policy =>
        {
            policy.AuthenticationSchemes.Add(Scheme);
            policy.RequireAuthenticatedUser();
        });
    }

    public static void AddSecurityForKeepFe(this IServiceCollection services)
    {
        services.AddKeepFeJwtAuthentication();
        services.AddKeepFeJwtAuthorization();
    }
}