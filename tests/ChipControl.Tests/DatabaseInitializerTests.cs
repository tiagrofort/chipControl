using ChipControl.Application;
using ChipControl.Domain.Entities;
using ChipControl.Domain.Enums;
using ChipControl.Domain.Interfaces;
using ChipControl.Infrastructure;
using ChipControl.Infrastructure.Configuration;
using ChipControl.Infrastructure.Data;
using ChipControl.Infrastructure.DependencyInjection;
using ChipControl.Infrastructure.Security;
using ChipControl.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ChipControl.Tests;

public class DatabaseInitializerTests : IDisposable
{
    private readonly string _tempDir;

    public DatabaseInitializerTests()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), "ChipControlTests_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_tempDir);
        DatabaseInitializer.ResetForTesting();
    }

    public void Dispose()
    {
        DatabaseInitializer.ResetForTesting();
        try
        {
            if (Directory.Exists(_tempDir))
                Directory.Delete(_tempDir, true);
        }
        catch { }
    }

    private string DbPath => Path.Combine(_tempDir, "test.db");
    private string ConfigPath => Path.Combine(_tempDir, "database.json");

    private IServiceProvider BuildServices()
    {
        var dbConfig = new DatabaseConfig
        {
            Provider = "SQLite",
            ConnectionString = $"Data Source={DbPath}",
            Database = "chipcontrol"
        };

        var services = new ServiceCollection();
        services.AddSingleton(dbConfig);
        services.AddPersistence(dbConfig);
        services.AddInfrastructure();
        services.AddApplication();
        services.AddTransient<DatabaseInitializer>();
        return services.BuildServiceProvider();
    }

    [Fact]
    public async Task Test1_PrimeiraExecucao_CriaDiretorioConfigBancoMigrationsEAdmin()
    {
        Assert.False(Directory.Exists(_tempDir) && File.Exists(DbPath));

        var sp = BuildServices();
        var initializer = sp.GetRequiredService<DatabaseInitializer>();

        await initializer.InitializeAsync();

        Assert.True(File.Exists(DbPath), "Banco SQLite deve ter sido criado");

        using var scope = sp.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<ChipControlDbContext>();
        Assert.True(await ctx.Database.CanConnectAsync());

        var applied = await ctx.Database.GetAppliedMigrationsAsync();
        Assert.NotEmpty(applied);
        Assert.Contains("20260831124346_InitialCreate", applied);
        Assert.Contains("20260901121911_AddFuncionarioGerenciamento", applied);
        Assert.Contains("20260903133708_AddSimcardGerenciamento", applied);

        var repo = scope.ServiceProvider.GetRequiredService<IUsuarioRepository>();
        var admin = await repo.BuscarPorLoginAsync("admin");
        Assert.NotNull(admin);
        Assert.Equal("Administrador", admin.Nome);
        Assert.Equal(NivelAcesso.Administrador, admin.NivelAcesso);
        Assert.True(admin.Ativo);
    }

    [Fact]
    public async Task Test2_SegundaExecucao_NaoDuplicaTabelasOuAdmin()
    {
        var sp1 = BuildServices();
        await sp1.GetRequiredService<DatabaseInitializer>().InitializeAsync();

        System.Collections.Generic.List<string> tables1;
        using (var scope1 = sp1.CreateScope())
        {
            var ctx = scope1.ServiceProvider.GetRequiredService<ChipControlDbContext>();
            tables1 = await GetTableNamesAsync(ctx);
            var usuariosCount1 = await CountUsuariosAsync(ctx);
            Assert.Contains("UsuariosSistema", tables1);
            Assert.Contains("Funcionarios", tables1);
            Assert.Contains("Simcards", tables1);
            Assert.Equal(1, usuariosCount1);
        }

        DatabaseInitializer.ResetForTesting();
        var sp2 = BuildServices();
        await sp2.GetRequiredService<DatabaseInitializer>().InitializeAsync();

        using var scope2 = sp2.CreateScope();
        var ctx2 = scope2.ServiceProvider.GetRequiredService<ChipControlDbContext>();
        var tables2 = await GetTableNamesAsync(ctx2);
        var usuariosCount2 = await CountUsuariosAsync(ctx2);

        Assert.Equal(tables1, tables2);
        Assert.Equal(1, usuariosCount2);
    }

    [Fact]
    public async Task Test3_BancoExistenteAtualizado_IniciaNormalmente()
    {
        var sp1 = BuildServices();
        await sp1.GetRequiredService<DatabaseInitializer>().InitializeAsync();
        DatabaseInitializer.ResetForTesting();

        var sp2 = BuildServices();
        await sp2.GetRequiredService<DatabaseInitializer>().InitializeAsync();

        using var scope = sp2.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<ChipControlDbContext>();
        var pending = await ctx.Database.GetPendingMigrationsAsync();
        Assert.Empty(pending);
    }

    [Fact]
    public async Task Test4_BancoComMigrationPendente_AplicaPendente()
    {
        var bootstrap = new ServiceCollection();
        var dbCfg = new DatabaseConfig
        {
            Provider = "SQLite",
            ConnectionString = $"Data Source={DbPath}",
            Database = "chipcontrol"
        };
        bootstrap.AddSingleton(dbCfg);
        bootstrap.AddPersistence(dbCfg);

        var bootstrapSp = bootstrap.BuildServiceProvider();
        using (var scope = bootstrapSp.CreateScope())
        {
            var ctx = scope.ServiceProvider.GetRequiredService<ChipControlDbContext>();
            await Microsoft.EntityFrameworkCore.RelationalDatabaseFacadeExtensions.MigrateAsync(ctx.Database);
        }

        DatabaseInitializer.ResetForTesting();
        var sp2 = BuildServices();
        await sp2.GetRequiredService<DatabaseInitializer>().InitializeAsync();

        using var scope2 = sp2.CreateScope();
        var ctx2 = scope2.ServiceProvider.GetRequiredService<ChipControlDbContext>();
        var applied = await ctx2.Database.GetAppliedMigrationsAsync();
        Assert.Contains("20260831124346_InitialCreate", applied);
        Assert.Contains("20260831142129_AddUsuarioGerenciamento", applied);
        Assert.Contains("20260901121911_AddFuncionarioGerenciamento", applied);
    }

    [Fact]
    public async Task Test5_AdministradorJaExiste_NaoCriaSegundo()
    {
        var sp1 = BuildServices();
        await sp1.GetRequiredService<DatabaseInitializer>().InitializeAsync();

        using (var scope1 = sp1.CreateScope())
        {
            var ctx = scope1.ServiceProvider.GetRequiredService<ChipControlDbContext>();
            await ctx.Database.ExecuteSqlRawAsync(
                "INSERT INTO UsuariosSistema (Nome, Login, SenhaHash, NivelAcesso, Ativo, DataCadastro) " +
                "VALUES ('OutroAdmin', 'outro', 'hash', 'Usuario', 1, datetime('now'));");
        }

        DatabaseInitializer.ResetForTesting();
        var sp2 = BuildServices();
        await sp2.GetRequiredService<DatabaseInitializer>().InitializeAsync();

        using var scope2 = sp2.CreateScope();
        var repo = scope2.ServiceProvider.GetRequiredService<IUsuarioRepository>();
        var usuarios = (await repo.ListarAsync()).ToList();
        Assert.Equal(2, usuarios.Count);
        Assert.Single(usuarios, u => u.Login == "admin");
    }

    [Fact]
    public async Task Test6_DatabaseJsonExisteBancoNaoExiste_CriaBancoEAplicaMigrations()
    {
        File.WriteAllText(ConfigPath, "{\"Database\":{\"Provider\":\"SQLite\",\"ConnectionString\":\"Data Source=" + DbPath + "\",\"Database\":\"chipcontrol\"}}");
        Assert.True(File.Exists(ConfigPath));
        Assert.False(File.Exists(DbPath));

        var sp = BuildServices();
        await sp.GetRequiredService<DatabaseInitializer>().InitializeAsync();

        Assert.True(File.Exists(DbPath));
        using var scope = sp.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<ChipControlDbContext>();
        var applied = await ctx.Database.GetAppliedMigrationsAsync();
        Assert.NotEmpty(applied);
    }

    [Fact]
    public async Task Test7_SemConfiguracao_UsaConfiguracaoPadrao()
    {
        var dbConfig = new DatabaseConfig
        {
            Provider = "SQLite",
            ConnectionString = $"Data Source={DbPath}",
            Database = "chipcontrol"
        };
        var services = new ServiceCollection();
        services.AddSingleton(dbConfig);
        services.AddPersistence(dbConfig);
        services.AddInfrastructure();
        services.AddApplication();
        services.AddTransient<DatabaseInitializer>();

        var sp = services.BuildServiceProvider();
        await sp.GetRequiredService<DatabaseInitializer>().InitializeAsync();

        Assert.True(File.Exists(DbPath));
    }

    [Fact]
    public async Task Test8_ErroDeInicializacao_NaoDestroiDados()
    {
        var badServices = new ServiceCollection();
        var badConfig = new DatabaseConfig
        {
            Provider = "SQLite",
            ConnectionString = $"Data Source={Path.Combine(_tempDir, "x.db")};Mode=ReadOnly",
            Database = "chipcontrol"
        };
        badServices.AddSingleton(badConfig);
        badServices.AddPersistence(badConfig);
        badServices.AddInfrastructure();
        badServices.AddApplication();
        badServices.AddTransient<DatabaseInitializer>();
        var badSp = badServices.BuildServiceProvider();

        await Assert.ThrowsAnyAsync<Exception>(async () =>
        {
            await badSp.GetRequiredService<DatabaseInitializer>().InitializeAsync();
        });

        Assert.False(File.Exists(Path.Combine(_tempDir, "x.db")), "Nada deve ter sido criado em modo ReadOnly");
    }

    [Fact]
    public void Test9_EnsureCreatedNaoUtilizadoNoFluxo()
    {
        var sln = Directory.GetFiles(Path.Combine(GetSolutionRoot(), "src"), "*.cs", SearchOption.AllDirectories);
        foreach (var file in sln)
        {
            var content = File.ReadAllText(file);
            Assert.DoesNotContain("EnsureCreated", content);
        }
    }

    [Fact]
    public async Task Test10_DuasInicializacoesConsecutivas_NenhumaExcecao()
    {
        var sp1 = BuildServices();
        await sp1.GetRequiredService<DatabaseInitializer>().InitializeAsync();
        DatabaseInitializer.ResetForTesting();
        var sp2 = BuildServices();
        await sp2.GetRequiredService<DatabaseInitializer>().InitializeAsync();

        using var scope = sp2.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<ChipControlDbContext>();
        var tables = await GetTableNamesAsync(ctx);
        Assert.Single(tables, t => t == "UsuariosSistema");
        Assert.Single(tables, t => t == "Funcionarios");
        Assert.Single(tables, t => t == "Simcards");
        Assert.Single(tables, t => t == "__EFMigrationsHistory");

        var applied = await ctx.Database.GetAppliedMigrationsAsync();
        Assert.Equal(5, applied.Count());
    }

    [Fact]
    public async Task TestBonus_BancoLegadoSemMigrationsHistory_RegistraHistorico()
    {
        using (var conn = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={DbPath}"))
        {
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"CREATE TABLE UsuariosSistema (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL,
                Login TEXT NOT NULL,
                SenhaHash TEXT NOT NULL,
                Email TEXT,
                NivelAcesso TEXT NOT NULL,
                Ativo INTEGER NOT NULL,
                Observacoes TEXT,
                DataCadastro TEXT NOT NULL,
                DataAlteracao TEXT
            ); CREATE UNIQUE INDEX IX_UsuariosSistema_Login ON UsuariosSistema(Login);";
            await cmd.ExecuteNonQueryAsync();

            using var ins = conn.CreateCommand();
            ins.CommandText = "INSERT INTO UsuariosSistema (Nome, Login, SenhaHash, NivelAcesso, Ativo, DataCadastro) VALUES ('Legado', 'legado', 'hash', 'Administrador', 1, datetime('now'));";
            await ins.ExecuteNonQueryAsync();
        }

        var sp = BuildServices();
        await sp.GetRequiredService<DatabaseInitializer>().InitializeAsync();

        using var scope = sp.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<ChipControlDbContext>();
        var applied = await ctx.Database.GetAppliedMigrationsAsync();
        Assert.NotEmpty(applied);
        Assert.Contains("20260831124346_InitialCreate", applied);

        var pending = await ctx.Database.GetPendingMigrationsAsync();
        Assert.Empty(pending);
    }

    private static string GetSolutionRoot()
    {
        var dir = AppContext.BaseDirectory;
        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir, "ChipControl.Solution.sln")) ||
                Directory.Exists(Path.Combine(dir, ".git")))
                return dir;
            dir = Directory.GetParent(dir)?.FullName;
        }
        return AppContext.BaseDirectory;
    }

    private static async Task<System.Collections.Generic.List<string>> GetTableNamesAsync(ChipControlDbContext ctx)
    {
        var conn = ctx.Database.GetDbConnection();
        await conn.OpenAsync();
        try
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;";
            var list = new System.Collections.Generic.List<string>();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                list.Add(reader.GetString(0));
            return list;
        }
        finally { conn.Close(); }
    }

    private static async Task<int> CountUsuariosAsync(ChipControlDbContext ctx)
    {
        var conn = ctx.Database.GetDbConnection();
        await conn.OpenAsync();
        try
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM UsuariosSistema;";
            return Convert.ToInt32(await cmd.ExecuteScalarAsync() ?? 0);
        }
        finally { conn.Close(); }
    }
}


