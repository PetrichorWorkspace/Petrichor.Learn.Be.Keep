using Keep.Domain.NoteAggregate.Services;
using Keep.Domain.UserAggregate.Services;
using Keep.Driven.Auth0Identity;
using Keep.Driven.Auth0Identity.Services;
using Keep.Driven.NpgsqlPersistence;
using Keep.Driving.ForAuth0;
using Keep.Driving.ForKeepFe;
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
        services.AddAuth0IdentityService(configuration);
        #endregion
        
        #region Domain
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<INoteService, NoteService>();
        #endregion
        
        #region Driving
        services.AddSecurityForAuth0();
        services.AddSecurityForKeepFe();
        #endregion
    }
    
    private static void AddPersistenceService(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<PersistenceCtx>(
            options => options
                .UseNpgsql(configuration.GetSection(nameof(NpgsqlPersistenceSettings)).Get<NpgsqlPersistenceSettings>()!.ConnectionString)
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IPersistenceCtx, PersistenceCtx>();
    }

    private static void AddAuth0IdentityService(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<Auth0IdentityServiceSettings>(configuration.GetSection(nameof(Auth0IdentityServiceSettings)));
        services.AddSingleton<ITokenServiceSingleton, TokenServiceSingleton>();
    }
}