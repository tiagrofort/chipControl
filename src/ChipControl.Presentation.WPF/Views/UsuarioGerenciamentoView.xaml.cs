using ChipControl.Application.DTOs;
using ChipControl.Application.UseCases;
using ChipControl.Presentation.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChipControl.Presentation.WPF.Views;

public partial class UsuarioGerenciamentoView : UserControl
{
    private readonly UsuarioGerenciamentoViewModel _viewModel;
    private readonly IUsuarioUseCase _useCase;

    public UsuarioGerenciamentoView()
    {
        InitializeComponent();
        _useCase = App.ServiceProvider?.GetRequiredService<IUsuarioUseCase>()
            ?? throw new InvalidOperationException("ServiceProvider nao inicializado.");
        _viewModel = new UsuarioGerenciamentoViewModel(_useCase, (ex) =>
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
        if (sender is not Button btn || btn.Tag is not UsuarioDto usuario) return;
        _ = EditarUsuarioAsync(usuario);
    }

    private async Task EditarUsuarioAsync(UsuarioDto usuario)
    {
        var modal = new UsuarioModalView();
        modal.DataContext = new UsuarioModalViewModel(_useCase, usuario);
        modal.Owner = Window.GetWindow(this);
        bool? result = null;
        modal.ModalFechado += (s, ok) => result = ok;
        modal.ShowDialog();

        if (result == true)
            await _viewModel.CarregarAsync();
    }

    private async void AlternarAtivo_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button btn || btn.Tag is not UsuarioDto usuario) return;
        try
        {
            await _useCase.AlternarAtivoAsync(usuario.Id);
            await _viewModel.CarregarAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private async Task AbrirModalCriacao()
    {
        var modal = new UsuarioModalView();
        modal.DataContext = new UsuarioModalViewModel(_useCase);
        modal.Owner = Window.GetWindow(this);
        bool? result = null;
        modal.ModalFechado += (s, ok) => result = ok;
        modal.ShowDialog();

        if (result == true)
            await _viewModel.CarregarAsync();
    }
}
