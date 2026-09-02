using ChipControl.Application;
using ChipControl.Application.DTOs;
using ChipControl.Domain.Enums;
using ChipControl.Infrastructure;
using ChipControl.Infrastructure.Configuration;
using ChipControl.Infrastructure.Data;
using ChipControl.Infrastructure.DependencyInjection;
using ChipControl.Presentation.WPF;
using ChipControl.Presentation.WPF.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Xunit;

namespace ChipControl.Tests;

/// <summary>
/// Testes de navegação da apresentação WPF.
/// Reproduzem o fluxo real: DI idêntico ao App, MainWindow real e cliques reais
/// nos botões do menu lateral via routed event.
/// </summary>
public class NavegacaoWpfTests : IDisposable
{
    private readonly string _tempDir;
    private readonly object _lock = new();

    public NavegacaoWpfTests()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), "ChipControlNavTests_" + Guid.NewGuid().ToString("N"));
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

    private string DbPath => Path.Combine(_tempDir, "nav.db");

    [Fact]
    public void Navegacao_TodosOsItensDoMenu_AbreSemEncerrar()
    {
        Exception? excecao = null;
        var thread = new Thread(() =>
        {
            lock (_lock)
            {
                try
                {
                    ExecutarNavegacaoCompleta();
                }
                catch (Exception ex)
                {
                    excecao = ex;
                }
                finally
                {
                    if (System.Windows.Application.Current is App app)
                        app.Shutdown();
                }
            }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        if (!thread.Join(TimeSpan.FromSeconds(120)))
            throw new Xunit.Sdk.XunitException("Timeout: o teste de navegacao STA nao terminou.");

        if (excecao != null)
            throw excecao;
    }

    private void ExecutarNavegacaoCompleta()
    {
        if (System.Windows.Application.Current is null)
        {
            _ = new App();
        }

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
        services.AddSingleton<Func<UsuarioAutenticadoDto, MainWindow>>(sp =>
            usuario => new MainWindow(usuario));
        services.AddTransient<LoginWindow>();
        services.AddTransient<DatabaseInitializer>();

        var sp = services.BuildServiceProvider();

        var prop = typeof(App).GetProperty(nameof(App.ServiceProvider), BindingFlags.Public | BindingFlags.Static)
            ?? throw new InvalidOperationException("Propriedade App.ServiceProvider nao encontrada.");
        prop.SetValue(null, sp);

        var initializer = sp.GetRequiredService<DatabaseInitializer>();
        initializer.InitializeAsync().GetAwaiter().GetResult();

        var usuario = new UsuarioAutenticadoDto
        {
            Nome = "Administrador",
            Login = "admin",
            NivelAcesso = NivelAcesso.Administrador,
            IsMaster = false
        };

        var factory = sp.GetRequiredService<Func<UsuarioAutenticadoDto, MainWindow>>();
        var mainWindow = factory(usuario);
        mainWindow.Show();
        PumpDispatcher();

        var frame = (Frame)mainWindow.FindName("MainFrame");
        Assert.NotNull(frame);

        // Dashboard inicial deve estar aberto após o construtor.
        var conteudoInicial = frame.Content;
        Assert.NotNull(conteudoInicial);
        Assert.IsType<DashboardView>(frame.Content);

        var botoes = EncontrarBotoes(mainWindow).ToList();
        Assert.True(botoes.Count >= 9, $"Esperado ao menos 9 itens de menu, encontrando {botoes.Count}.");

        // Cada clique do menu deve navegar sem encerrar a aplicacao.
        foreach (var botao in botoes)
        {
            botao.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, botao));
            PumpDispatcher();
            Assert.NotNull(frame.Content);
            Assert.IsAssignableFrom<UserControl>(frame.Content);
        }

        // Garante que todas as telas esperadas foram abertas em algum momento.
        var tiposVisitados = new List<Type>();
        foreach (var botao in botoes)
        {
            botao.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, botao));
            PumpDispatcher();
            if (frame.Content is UserControl uc && !tiposVisitados.Contains(uc.GetType()))
                tiposVisitados.Add(uc.GetType());
        }

        Assert.Contains(typeof(DashboardView), tiposVisitados);
        Assert.Contains(typeof(PlaceholderView), tiposVisitados);
        Assert.Contains(typeof(FuncionarioGerenciamentoView), tiposVisitados);

        // 'Dashboard' nao deve fechar a aplicacao nem mesmo ao re-navegar.
        var botaoDashboard = botoes.First(b => string.Equals((string)b.Content, "Dashboard", StringComparison.OrdinalIgnoreCase));
        botaoDashboard.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, botaoDashboard));
        PumpDispatcher();
        Assert.IsType<DashboardView>(frame.Content);

        // SIMCARDs (placeholder) tambem deve permanecer estavel.
        var botaoSimcards = botoes.First(b => string.Equals((string)b.Content, "SIMCARDs", StringComparison.OrdinalIgnoreCase));
        botaoSimcards.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, botaoSimcards));
        PumpDispatcher();
        Assert.IsType<PlaceholderView>(frame.Content);

        // Fecha a janela para retomar o appraisal da aplicacao.
        mainWindow.Close();
        PumpDispatcher();
    }
[Fact]
    public void PlaceholderView_RecebeTitulo_E_ExibePlaceholder()
    {
        ExecutarEmSta(() =>
        {
            var view = new PlaceholderView("SIMCARDs");
            Assert.Equal("SIMCARDs", view.Titulo);
            Assert.Same(view, view.DataContext);
        });
    }

    [Fact]
    public void DashboardView_DevePoderSerCriada()
    {
        ExecutarEmSta(() =>
        {
            var view = new DashboardView();
            Assert.NotNull(view);
        });
    }

    [Fact]
    public void FuncionarioGerenciamentoView_DependenciasRegistradasNoDI()
    {
        // Verifica que as dependencias necessarias para a tela Funcionarios
        // existem no conjunto registrado pelo App.
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

        using var sp = services.BuildServiceProvider();
        Exception? erro = null;
        try
        {
            _ = sp.GetRequiredService<Application.UseCases.IFuncionarioUseCase>();
            _ = sp.GetRequiredService<Application.UseCases.IUsuarioUseCase>();
            _ = sp.GetRequiredService<Application.UseCases.IAutenticarUsuarioUseCase>();
        }
        catch (Exception ex)
        {
            erro = ex;
        }

        Assert.Null(erro);
    }

    private static void PumpDispatcher()
    {
        var frame = new DispatcherFrame();
        Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => frame.Continue = false));
        Dispatcher.PushFrame(frame);
    }

    private static void ExecutarEmSta(Action action)
    {
        Exception? excecao = null;
        var thread = new Thread(() =>
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                excecao = ex;
            }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        if (!thread.Join(TimeSpan.FromSeconds(60)))
            throw new Xunit.Sdk.XunitException("Timeout: o teste STA nao terminou.");

        if (excecao != null)
            throw excecao;
    }

    private static IEnumerable<Button> EncontrarBotoes(DependencyObject root)
    {
        var pilha = new Stack<DependencyObject>();
        pilha.Push(root);
        while (pilha.Count > 0)
        {
            var atual = pilha.Pop();
            if (atual is Button botao)
                yield return botao;

            var count = VisualTreeHelper.GetChildrenCount(atual);
            for (var i = 0; i < count; i++)
                pilha.Push(VisualTreeHelper.GetChild(atual, i));
        }
    }
}