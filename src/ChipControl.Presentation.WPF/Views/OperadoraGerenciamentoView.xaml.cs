using ChipControl.Application.DTOs;
using ChipControl.Application.UseCases;
using ChipControl.Presentation.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChipControl.Presentation.WPF.Views;

public partial class OperadoraGerenciamentoView : UserControl
{
    private readonly OperadoraGerenciamentoViewModel _viewModel;
    private readonly IOperadoraUseCase _useCase;

    public OperadoraGerenciamentoView()
    {
        InitializeComponent();
        _useCase = App.ServiceProvider?.GetRequiredService<IOperadoraUseCase>()
            ?? throw new InvalidOperationException("ServiceProvider nao inicializado.");
        _viewModel = new OperadoraGerenciamentoViewModel(_useCase, (ex) =>
            MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error));
        DataContext = _viewModel;
        Loaded += async (s, e) => await _viewModel.CarregarAsync();
    }

    private async void Novo_Click(object sender, RoutedEventArgs e)
    {
        await AbrirModalCriacao();
    }

    private async void Refresh_Click(object sender, RoutedEventArgs e)
    {
        await _viewModel.CarregarAsync();
    }

    private void Editar_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button btn || btn.Tag is not OperadoraDto operadora) return;
        _ = EditarOperadoraAsync(operadora);
    }

    private async Task EditarOperadoraAsync(OperadoraDto operadora)
    {
        var modal = new OperadoraModalView();
        modal.DataContext = new OperadoraModalViewModel(_useCase, operadora);
        modal.Owner = Window.GetWindow(this);
        bool? result = null;
        modal.ModalFechado += (s, ok) => result = ok;
        modal.ShowDialog();

        if (result == true)
            await _viewModel.CarregarAsync();
    }

    private async void AlternarAtivo_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button btn || btn.Tag is not OperadoraDto operadora) return;
        try
        {
            await _useCase.AlternarAtivoAsync(operadora.Id);
            await _viewModel.CarregarAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private async Task AbrirModalCriacao()
    {
        var modal = new OperadoraModalView();
        modal.DataContext = new OperadoraModalViewModel(_useCase);
        modal.Owner = Window.GetWindow(this);
        bool? result = null;
        modal.ModalFechado += (s, ok) => result = ok;
        modal.ShowDialog();

        if (result == true)
            await _viewModel.CarregarAsync();
    }
}