namespace ChipControl.Application;

using ChipControl.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAutenticarUsuarioUseCase, AutenticarUsuarioUseCase>();
        services.AddScoped<ICriarAdministradorInicialUseCase, CriarAdministradorInicialUseCase>();
        services.AddScoped<IUsuarioUseCase, UsuarioUseCase>();
        services.AddScoped<IFuncionarioUseCase, FuncionarioUseCase>();
        services.AddScoped<ISimcardUseCase, SimcardUseCase>();

        return services;
    }
}
