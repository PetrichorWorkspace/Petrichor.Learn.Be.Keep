using Keep.Domain.Services;
using Keep.Driven.NpgsqlPersistence;
using Keep.Driving.Common.Security;
using Microsoft.EntityFrameworkCore;
using Shared.Core;

namespace Keep;

public static class ServiceCollectionExt
{
    public static void AddAllServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        #region SharedCore
        services.AddSharedCoreService(typeof(Program).Assembly);
        #endregion
        
        #region Network
        services.AddEndpointsApiExplorer();
        #endregion
        
        #region Helper
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddSwaggerGen();
        #endregion
        
        #region Drivens

        services.AddPersistenceService(configuration);
        
        services.AddAuth0IdentityService();

        #endregion
        
        #region Domain
        services.AddDomainServices();
        #endregion
        
        #region Driving
        services.AddSecurityConfigurations();
        #endregion
    }
    
    private static void AddSecurityConfigurations(this IServiceCollection services)
    {
        services.AddAuthentication()
            .AddAuth0ApiKeyAuthentication();

        services.AddAuthorizationBuilder()
            .AddAuth0ApiKeyAuthorization();
    }
    
    private static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }
    
    private static void AddPersistenceService(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<PersistenceCtx>(
            options => options
                .UseNpgsql(configuration.GetSection(nameof(NpgsqlPersistenceSettings)).Get<NpgsqlPersistenceSettings>()!.ConnectionString)
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IPersistenceCtx, PersistenceCtx>();
    }

    private static void AddAuth0IdentityService(this IServiceCollection services)
    {
    }
}