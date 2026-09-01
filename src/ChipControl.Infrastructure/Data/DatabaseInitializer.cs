using ChipControl.Domain.Entities;
using ChipControl.Domain.Enums;
using ChipControl.Domain.Interfaces;
using ChipControl.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ChipControl.Infrastructure.Data;

public class DatabaseInitializer
{
    private readonly IServiceProvider _serviceProvider;
    private static readonly SemaphoreSlim _initLock = new(1, 1);
    private static int _initialized;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task InitializeAsync()
    {
        if (Volatile.Read(ref _initialized) == 1)
            return;

        await _initLock.WaitAsync();
        try
        {
            if (Volatile.Read(ref _initialized) == 1)
                return;

            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ChipControlDbContext>();
            var dbPath = ExtractDataSource(context.Database.GetConnectionString());

            Log($"Inicializando banco de dados. Provider={context.Database.ProviderName} DataSource={dbPath}");

            await EnsureMigrationHistoryIfLegacyDatabaseAsync(context);

            var canConnect = await context.Database.CanConnectAsync();
            Log($"Banco acessivel: {canConnect}");

            var pending = await context.Database.GetPendingMigrationsAsync();
            var applied = await context.Database.GetAppliedMigrationsAsync();

            Log($"Migrations aplicadas: {string.Join(",", applied)}. Migrations pendentes: {string.Join(",", pending)}");

            if (!canConnect)
            {
                Log("Banco nao existe. Sera criado via Migrate.");
            }

            await context.Database.MigrateAsync();

            Log("Migrate concluido.");

            await EnsureInitialAdminAsync(scope.ServiceProvider);

            Volatile.Write(ref _initialized, 1);
        }
        finally
        {
            _initLock.Release();
        }
    }

    public static void ResetForTesting()
    {
        Volatile.Write(ref _initialized, 0);
    }

    private async Task EnsureInitialAdminAsync(IServiceProvider sp)
    {
        var repo = sp.GetRequiredService<IUsuarioRepository>();
        var hash = sp.GetRequiredService<IHashService>();

        if (await repo.ExisteLoginAsync("admin"))
        {
            Log("Administrador inicial ja existe. Nenhum novo usuario sera criado.");
            return;
        }

        var senhaHash = hash.Hash("admin123");
        var admin = UsuarioSistema.Create(
            nome: "Administrador",
            login: "admin",
            senhaHash: senhaHash,
            nivelAcesso: NivelAcesso.Administrador,
            email: null,
            observacoes: "Administrador inicial criado automaticamente na primeira execucao.");

        await repo.AdicionarAsync(admin);
        Log("Administrador inicial criado.");
    }

    private async Task EnsureMigrationHistoryIfLegacyDatabaseAsync(ChipControlDbContext context)
    {
        var conn = context.Database.GetDbConnection();
        var wasOpen = conn.State == System.Data.ConnectionState.Open;
        if (!wasOpen) await conn.OpenAsync();

        try
        {
            using (var checkCmd = conn.CreateCommand())
            {
                checkCmd.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='__EFMigrationsHistory';";
                var count = Convert.ToInt32(await checkCmd.ExecuteScalarAsync() ?? 0);
                if (count > 0)
                {
                    Log("Tabela __EFMigrationsHistory ja existe.");
                    return;
                }
            }

            using (var checkCmd = conn.CreateCommand())
            {
                checkCmd.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='UsuariosSistema';";
                var usuariosCount = Convert.ToInt32(await checkCmd.ExecuteScalarAsync() ?? 0);
                if (usuariosCount == 0)
                {
                    Log("Banco vazio. Nenhum legado detectado.");
                    return;
                }
            }

            Log("AVISO: Banco legado detectado (tabelas existem mas __EFMigrationsHistory nao). Registrando migrations ja aplicadas.");

            var migrationsToRegister = new[]
            {
                "20260831124346_InitialCreate",
                "20260831142129_AddUsuarioGerenciamento",
                "20260901121911_AddFuncionarioGerenciamento"
            };

            using var createCmd = conn.CreateCommand();
            createCmd.CommandText = @"CREATE TABLE IF NOT EXISTS __EFMigrationsHistory (
                MigrationId TEXT NOT NULL PRIMARY KEY,
                ProductVersion TEXT NOT NULL
            );";
            await createCmd.ExecuteNonQueryAsync();

            foreach (var migration in migrationsToRegister)
            {
                using var insertCmd = conn.CreateCommand();
                insertCmd.CommandText = "INSERT OR IGNORE INTO __EFMigrationsHistory (MigrationId, ProductVersion) VALUES ($id, $ver);";
                var pId = insertCmd.CreateParameter(); pId.ParameterName = "$id"; pId.Value = migration;
                var pVer = insertCmd.CreateParameter(); pVer.ParameterName = "$ver"; pVer.Value = "8.0.0";
                insertCmd.Parameters.Add(pId);
                insertCmd.Parameters.Add(pVer);
                await insertCmd.ExecuteNonQueryAsync();
            }

            Log("Migrations legadas registradas no historico.");
        }
        finally
        {
            if (!wasOpen) await conn.CloseAsync();
        }
    }

    private static string ExtractDataSource(string? connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString)) return "(empty)";
        foreach (var part in connectionString.Split(';', StringSplitOptions.RemoveEmptyEntries))
        {
            var trimmed = part.Trim();
            if (trimmed.StartsWith("Data Source=", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("DataSource=", StringComparison.OrdinalIgnoreCase))
            {
                return trimmed[(trimmed.IndexOf('=') + 1)..];
            }
        }
        return connectionString;
    }

    private static void Log(string message)
    {
        Debug.WriteLine($"[ChipControl] {message}");
    }
}
