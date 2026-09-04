namespace ChipControl.Infrastructure.DependencyInjection;

using ChipControl.Domain.Interfaces;
using ChipControl.Infrastructure.Data.Repositories;
using ChipControl.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
        services.AddScoped<ISimcardRepository, SimcardRepository>();
        services.AddScoped<IOperadoraRepository, OperadoraRepository>();
        services.AddSingleton<IHashService, HashService>();

        return services;
    }
}
