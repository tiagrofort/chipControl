using ChipControl.Application.DTOs;
using ChipControl.Presentation.WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ChipControl.Presentation.WPF.Views;

public partial class MainWindow : Window
{
    public MainWindow(UsuarioAutenticadoDto usuario)
    {
        InitializeComponent();
        var viewModel = new MainViewModel(usuario);
        DataContext = viewModel;
        MainFrame.Navigate(new Views.DashboardView());
    }

    private void Dashboard_Click(object sender, RoutedEventArgs e) => Navegar(new Views.DashboardView());
    private void Simcards_Click(object sender, RoutedEventArgs e) => Navegar(new Views.PlaceholderView("SIMCARDs"));
    private void Funcionarios_Click(object sender, RoutedEventArgs e) => Navegar(new Views.PlaceholderView("Funcionários"));
    private void Operadoras_Click(object sender, RoutedEventArgs e) => Navegar(new Views.PlaceholderView("Operadoras"));
    private void Aparelhos_Click(object sender, RoutedEventArgs e) => Navegar(new Views.PlaceholderView("Aparelhos"));
    private void TrocaNumeros_Click(object sender, RoutedEventArgs e) => Navegar(new Views.PlaceholderView("Troca de Números"));
    private void Substituicao_Click(object sender, RoutedEventArgs e) => Navegar(new Views.PlaceholderView("Substituição"));
    private void Historico_Click(object sender, RoutedEventArgs e) => Navegar(new Views.PlaceholderView("Histórico"));
    private void Relatorios_Click(object sender, RoutedEventArgs e) => Navegar(new Views.PlaceholderView("Relatórios"));

    private void Navegar(UserControl view)
    {
        MainFrame.Navigate(view);
    }
}
