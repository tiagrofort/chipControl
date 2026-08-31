namespace ChipControl.Application;

using ChipControl.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAutenticarUsuarioUseCase, AutenticarUsuarioUseCase>();
        services.AddScoped<ICriarAdministradorInicialUseCase, CriarAdministradorInicialUseCase>();

        return services;
    }
}
