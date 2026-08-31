using ChipControl.Infrastructure.Configuration;
using ChipControl.Infrastructure.Data.Providers;
using ChipControl.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChipControl.Infrastructure;

public static class PersistenceServiceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, DatabaseConfig dbConfig)
    {
        DatabaseProviderFactory.ConfigureDbContext(services, dbConfig.Provider, dbConfig.ConnectionString);
        services.AddScoped<ChipControlDbContext>();
        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, string configPath)
    {
        var dbConfig = DatabaseConfigManager.Load(configPath);
        return services.AddPersistence(dbConfig);
    }

    public static DatabaseConfig LoadConfiguration(string configPath)
    {
        return DatabaseConfigManager.Load(configPath);
    }
}
