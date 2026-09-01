using ChipControl.Application.DTOs;
using ChipControl.Application.UseCases;
using ChipControl.Presentation.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChipControl.Presentation.WPF.Views;

public partial class FuncionarioGerenciamentoView : UserControl
{
    private readonly FuncionarioGerenciamentoViewModel _viewModel;
    private readonly IFuncionarioUseCase _useCase;

    public FuncionarioGerenciamentoView()
    {
        InitializeComponent();
        _useCase = App.ServiceProvider?.GetRequiredService<IFuncionarioUseCase>()
            ?? throw new InvalidOperationException("ServiceProvider nao inicializado.");
        _viewModel = new FuncionarioGerenciamentoViewModel(_useCase, (ex) =>
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
        if (sender is not Button btn || btn.Tag is not FuncionarioDto funcionario) return;
        _ = EditarFuncionarioAsync(funcionario);
    }

    private async Task EditarFuncionarioAsync(FuncionarioDto funcionario)
    {
        var modal = new FuncionarioModalView();
        modal.DataContext = new FuncionarioModalViewModel(_useCase, funcionario);
        modal.Owner = Window.GetWindow(this);
        bool? result = null;
        modal.ModalFechado += (s, ok) => result = ok;
        modal.ShowDialog();

        if (result == true)
            await _viewModel.CarregarAsync();
    }

    private async void AlternarAtivo_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button btn || btn.Tag is not FuncionarioDto funcionario) return;
        try
        {
            await _useCase.AlternarAtivoAsync(funcionario.Id);
            await _viewModel.CarregarAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private async Task AbrirModalCriacao()
    {
        var modal = new FuncionarioModalView();
        modal.DataContext = new FuncionarioModalViewModel(_useCase);
        modal.Owner = Window.GetWindow(this);
        bool? result = null;
        modal.ModalFechado += (s, ok) => result = ok;
        modal.ShowDialog();

        if (result == true)
            await _viewModel.CarregarAsync();
    }
}
