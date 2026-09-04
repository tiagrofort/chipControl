using ChipControl.Application.DTOs;
using ChipControl.Application.UseCases;
using ChipControl.Domain.Enums;
using ChipControl.Presentation.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChipControl.Presentation.WPF.Views;

public partial class SimcardGerenciamentoView : UserControl
{
    private readonly SimcardGerenciamentoViewModel _viewModel;
    private readonly ISimcardUseCase _useCase;

    public SimcardGerenciamentoView()
    {
        InitializeComponent();
        _useCase = App.ServiceProvider?.GetRequiredService<ISimcardUseCase>()
            ?? throw new InvalidOperationException("ServiceProvider nao inicializado.");
        _viewModel = new SimcardGerenciamentoViewModel(_useCase, (ex) =>
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
        if (sender is not Button btn || btn.Tag is not SimcardDto simcard) return;
        _ = EditarSimcardAsync(simcard);
    }

    private async Task EditarSimcardAsync(SimcardDto simcard)
    {
        var modal = new SimcardModalView();
        var vm = new SimcardModalViewModel(_useCase, simcard);
        modal.DataContext = vm;
        modal.Owner = Window.GetWindow(this);
        bool? result = null;
        modal.ModalFechado += (s, ok) => result = ok;
        await vm.CarregarOperadorasAsync();
        modal.ShowDialog();

        if (result == true)
            await _viewModel.CarregarAsync();
    }

    private async void AlternarAtivo_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button btn || btn.Tag is not SimcardDto simcard) return;
        try
        {
            await _useCase.AlternarAtivoAsync(simcard.Id);
            await _viewModel.CarregarAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private async void StatusMenuItem_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not MenuItem menuItem || menuItem.Tag is null) return;
        if (!Enum.TryParse<SimcardStatus>(menuItem.Tag.ToString(), out var novoStatus)) return;

        if (_viewModel.SimcardSelecionado is not SimcardDto simcard) return;
        try
        {
            await _useCase.AlterarStatusAsync(simcard.Id, novoStatus);
            await _viewModel.CarregarAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private async Task AbrirModalCriacao()
    {
        var modal = new SimcardModalView();
        var vm = new SimcardModalViewModel(_useCase);
        modal.DataContext = vm;
        modal.Owner = Window.GetWindow(this);
        bool? result = null;
        modal.ModalFechado += (s, ok) => result = ok;
        await vm.CarregarOperadorasAsync();
        modal.ShowDialog();

        if (result == true)
            await _viewModel.CarregarAsync();
    }
}
