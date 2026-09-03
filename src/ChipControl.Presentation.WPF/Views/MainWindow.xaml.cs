using ChipControl.Application.DTOs;
using ChipControl.Presentation.WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ChipControl.Presentation.WPF.Views;

public partial class MainWindow : Window
{
    private readonly UsuarioAutenticadoDto _usuario;

    private readonly Button[] _navButtons = null!;

    public MainWindow(UsuarioAutenticadoDto usuario)
    {
        InitializeComponent();
        _usuario = usuario;
        var viewModel = new MainViewModel(usuario);
        DataContext = viewModel;

        _navButtons = new[]
        {
            NavDashboard, NavSimcards, NavFuncionarios, NavOperadoras, NavAparelhos,
            NavTrocaNumeros, NavSubstituicao, NavHistorico, NavRelatorios, NavUsuarios
        };

        Navegar(new DashboardView(), "Dashboard", NavDashboard);
    }

    private void Dashboard_Click(object sender, RoutedEventArgs e) => Navegar(new DashboardView(), "Dashboard", NavDashboard);
    private void Simcards_Click(object sender, RoutedEventArgs e) => Navegar(new PlaceholderView("SIMCARDs"), "SIMCARDs", NavSimcards);
    private void Funcionarios_Click(object sender, RoutedEventArgs e) => Navegar(new FuncionarioGerenciamentoView(), "Funcionários", NavFuncionarios);
    private void Operadoras_Click(object sender, RoutedEventArgs e) => Navegar(new PlaceholderView("Operadoras"), "Operadoras", NavOperadoras);
    private void Aparelhos_Click(object sender, RoutedEventArgs e) => Navegar(new PlaceholderView("Aparelhos"), "Aparelhos", NavAparelhos);
    private void TrocaNumeros_Click(object sender, RoutedEventArgs e) => Navegar(new PlaceholderView("Troca de Números"), "Troca de Números", NavTrocaNumeros);
    private void Substituicao_Click(object sender, RoutedEventArgs e) => Navegar(new PlaceholderView("Substituição"), "Substituição", NavSubstituicao);
    private void Historico_Click(object sender, RoutedEventArgs e) => Navegar(new PlaceholderView("Histórico"), "Histórico", NavHistorico);
    private void Relatorios_Click(object sender, RoutedEventArgs e) => Navegar(new PlaceholderView("Relatórios"), "Relatórios", NavRelatorios);
    private void Usuarios_Click(object sender, RoutedEventArgs e) => Navegar(new UsuarioGerenciamentoView(), "Usuários do Sistema", NavUsuarios);

    private void Navegar(UserControl view, string titulo, Button navButton)
    {
        MainFrame.Navigate(view);
        HeaderTitleText.Text = titulo;
        AtualizarSelecao(navButton);
    }

    private void AtualizarSelecao(Button selecionado)
    {
        foreach (var botao in _navButtons)
        {
            bool ativo = ReferenceEquals(botao, selecionado);
            botao.Style = (Style)FindResource(ativo ? "NavItemButtonSelected" : "NavItemButton");
        }
    }
}
