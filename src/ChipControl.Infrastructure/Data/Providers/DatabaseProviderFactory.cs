namespace ChipControl.Infrastructure.Data.Providers;

using ChipControl.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Factory para configuracao do DbContext baseado no provider escolhido.
///
/// DECISAO: Apenas SQLite esta implementado nesta etapa.
/// PostgreSQL e MySQL exigem pacotes Npgsql e Pomelo MySql respectivamente,
/// que serao adicionados quando houver necessidade (ver docs/09-BACKLOG-FUTURO.md).
/// A estrutura do factory ja existe e esta preparada para extensao futura.
/// </summary>
public static class DatabaseProviderFactory
{
    public static void ConfigureDbContext(IServiceCollection services, string provider, string connectionString)
    {
        switch (provider?.ToUpperInvariant())
        {
            case "SQLITE":
                services.AddDbContext<ChipControlDbContext>(options =>
                    options.UseSqlite(connectionString));
                break;
            case "POSTGRESQL":
                throw new NotSupportedException(
                    "PostgreSQL sera suportado futuramente. Instale o provider Npgsql.");
            case "MYSQL":
                throw new NotSupportedException(
                    "MySQL sera suportado futuramente. Instale o provider Pomelo MySql.");
            default:
                throw new NotSupportedException($"Provider '{provider}' nao suportado.");
        }
    }
}
