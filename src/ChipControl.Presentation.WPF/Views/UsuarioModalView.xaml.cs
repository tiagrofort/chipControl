using ChipControl.Domain.Enums;
using ChipControl.Presentation.WPF.ViewModels;
using System;
using System.Windows;

namespace ChipControl.Presentation.WPF.Views;

public partial class UsuarioModalView : Window
{
    public event EventHandler<bool>? ModalFechado;

    public UsuarioModalView()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is UsuarioModalViewModel vm)
        {
            NivelCombo.ItemsSource = Enum.GetValues<NivelAcesso>();
            NivelCombo.SelectedValue = vm.NivelAcesso;
        }
    }

    private async void Salvar_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is not UsuarioModalViewModel vm) return;

        vm.Senha = SenhaBox.Password;
        vm.ConfirmarSenha = ConfirmarSenhaBox.Password;

        if (NivelCombo.SelectedValue is NivelAcesso nivel)
            vm.NivelAcesso = nivel;

        var ok = await vm.SalvarAsync();
        ModalFechado?.Invoke(this, ok);
        Close();
    }

    private void Cancelar_Click(object sender, RoutedEventArgs e)
    {
        ModalFechado?.Invoke(this, false);
        Close();
    }
}
