using ChipControl.Infrastructure.Configuration;
using ChipControl.Infrastructure.DependencyInjection;
using ChipControl.Presentation.WPF.Services;
using ChipControl.Presentation.WPF.ViewModels;
using ChipControl.Presentation.WPF.Views;
using ChipControl.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using ChipControl.Infrastructure;
using ChipControl.Application;

namespace ChipControl.Presentation.WPF;

public partial class App : System.Windows.Application
{
    public static IServiceProvider? ServiceProvider { get; private set; }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var configPath = DatabaseConfigPaths.GetConfigPath();
        if (!DatabaseConfigPaths.ConfigExists())
            await CreateDefaultConfigAsync(configPath);

        try
        {
            var dbConfig = DatabaseConfigManager.Load(configPath);

            var services = new ServiceCollection();
            services.AddSingleton(dbConfig);
            services.AddPersistence(dbConfig);
            services.AddInfrastructure();
            services.AddApplication();
            services.AddTransient<LoginWindow>();
            services.AddTransient<DatabaseInitializer>();

            ServiceProvider = services.BuildServiceProvider();

            await EnsureDatabaseCreated();

            var loginWindow = ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro na inicializacao: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            Shutdown();
        }
    }

    private static async Task EnsureDatabaseCreated()
    {
        if (ServiceProvider == null) return;

        using var scope = ServiceProvider.CreateScope();
        var provider = scope.ServiceProvider;

        var initializer = provider.GetRequiredService<DatabaseInitializer>();
        await initializer.EnsureSeedAsync();
    }

    private static async Task CreateDefaultConfigAsync(string configPath)
    {
        var dbPath = DatabaseConfigPaths.GetDefaultSqlitePath();
        var dbConfig = new DatabaseConfig
        {
            Provider = "SQLite",
            ConnectionString = $"Data Source={dbPath}",
            Database = "chipcontrol"
        };
        DatabaseConfigManager.Save(configPath, dbConfig);
        await Task.CompletedTask;
    }

    protected override void OnExit(ExitEventArgs e)
    {
        if (ServiceProvider is IDisposable disposable)
            disposable.Dispose();
        base.OnExit(e);
    }
}
