using ChipControl.Application;
using ChipControl.Application.DTOs;
using ChipControl.Infrastructure;
using ChipControl.Infrastructure.Configuration;
using ChipControl.Infrastructure.Data;
using ChipControl.Infrastructure.DependencyInjection;
using ChipControl.Presentation.WPF.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace ChipControl.Presentation.WPF;

public partial class App : System.Windows.Application
{
    public static IServiceProvider? ServiceProvider { get; private set; }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        ConfigurarTratamentoGlobalDeExcecoes();

        var configPath = DatabaseConfigPaths.GetConfigPath();

        try
        {
            if (!File.Exists(configPath))
            {
                Log($"database.json nao encontrado em {configPath}. Criando configuracao padrao SQLite.");
                await CreateDefaultConfigAsync(configPath);
            }
            else
            {
                Log($"database.json localizado em {configPath}.");
            }

            var dbConfig = DatabaseConfigManager.Load(configPath);
            Log($"Provider={dbConfig.Provider}");

            var services = new ServiceCollection();
            services.AddSingleton(dbConfig);
            services.AddPersistence(dbConfig);
            services.AddInfrastructure();
            services.AddApplication();
            services.AddSingleton<Func<UsuarioAutenticadoDto, MainWindow>>(sp =>
                usuario => new MainWindow(usuario));
            services.AddTransient<LoginWindow>();
            services.AddTransient<DatabaseInitializer>();

            ServiceProvider = services.BuildServiceProvider();

            var initializer = ServiceProvider.GetRequiredService<DatabaseInitializer>();
            await initializer.InitializeAsync();

            var loginWindow = ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
        }
        catch (Exception ex)
        {
            Log($"ERRO na inicializacao: {ex}");
            MessageBox.Show(
                "Nao foi possivel inicializar o banco de dados do ChipControl.\n\nDetalhe tecnico: " + ex.Message,
                "ChipControl",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            Shutdown();
        }
    }

    private static void Log(string message)
    {
        Debug.WriteLine($"[ChipControl] {message}");
    }

    private void ConfigurarTratamentoGlobalDeExcecoes()
    {
        DispatcherUnhandledException += (_, args) =>
        {
            Log($"[EXCECAO NAO TRATADA no Dispatcher] {args.Exception}");
            MostrarMensagemAmigavel();
            args.Handled = true; // Mantem o processo vivo sempre que possivel.
        };

        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
            Log($"[EXCECAO NAO TRATADA no AppDomain] {args.ExceptionObject}");

        TaskScheduler.UnobservedTaskException += (_, args) =>
        {
            Log($"[EXCECAO NAO TRATADA em Task] {args.Exception}");
            args.SetObserved();
        };
    }

    private static void MostrarMensagemAmigavel()
    {
        try
        {
            MessageBox.Show(
                "Nao foi possivel abrir esta tela. O detalhe tecnico foi registrado para diagnostico.",
                "ChipControl",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
        catch
        {
            // Aplicacao em finalizacao; nao exibir nada.
        }
    }

    private static async Task CreateDefaultConfigAsync(string configPath)
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var appFolder = Path.Combine(appData, "ChipControl");
        if (!Directory.Exists(appFolder))
        {
            Directory.CreateDirectory(appFolder);
            Log($"Diretorio criado: {appFolder}");
        }

        var dbPath = Path.Combine(appFolder, "chipcontrol.db");
        var dbConfig = new DatabaseConfig
        {
            Provider = "SQLite",
            ConnectionString = $"Data Source={dbPath}",
            Database = "chipcontrol"
        };
        DatabaseConfigManager.Save(configPath, dbConfig);
        Log($"database.json criado em {configPath}. Banco SQLite sera criado em {dbPath}.");
        await Task.CompletedTask;
    }

    protected override void OnExit(ExitEventArgs e)
    {
        if (ServiceProvider is IDisposable disposable)
            disposable.Dispose();
        base.OnExit(e);
    }
}
