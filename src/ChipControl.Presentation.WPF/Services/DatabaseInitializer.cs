using ChipControl.Domain;
using ChipControl.Domain.Entities;
using ChipControl.Domain.Enums;
using ChipControl.Domain.Interfaces;
using ChipControl.Presentation.WPF.Services;
using ChipControl.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace ChipControl.Presentation.WPF.Services;

public class DatabaseInitializer
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task EnsureSeedAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ChipControlDbContext>();
        await context.Database.EnsureCreatedAsync();

        var pending = await context.Database.GetPendingMigrationsAsync();
        if (pending.Any())
            await context.Database.MigrateAsync();

        var repo = scope.ServiceProvider.GetRequiredService<IUsuarioRepository>();
        var hashService = scope.ServiceProvider.GetRequiredService<IHashService>();

        var count = await repo.CountAsync();
        if (count == 0)
        {
            var senhaHash = hashService.Hash("admin123");
            var admin = UsuarioSistema.Create(
                nome: "Administrador",
                login: "admin",
                senhaHash: senhaHash,
                nivelAcesso: NivelAcesso.Administrador,
                email: null,
                observacoes: "Administrador inicial criado automaticamente na primeira execucao.");

            await repo.AdicionarAsync(admin);
        }
    }
}
