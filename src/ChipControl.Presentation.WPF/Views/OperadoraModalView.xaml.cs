using ChipControl.Presentation.WPF.ViewModels;
using System;
using System.Windows;

namespace ChipControl.Presentation.WPF.Views;

public partial class OperadoraModalView : Window
{
    public event EventHandler<bool>? ModalFechado;

    public OperadoraModalView()
    {
        InitializeComponent();
    }

    private async void Salvar_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is not OperadoraModalViewModel vm) return;

        var ok = await vm.SalvarAsync();
        ModalFechado?.Invoke(this, ok);
        if (ok) Close();
    }

    private void Cancelar_Click(object sender, RoutedEventArgs e)
    {
        ModalFechado?.Invoke(this, false);
        Close();
    }
}