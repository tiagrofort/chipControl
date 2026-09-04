using System;
using System.Windows;

namespace ChipControl.Presentation.WPF.Views;

public partial class SimcardModalView : Window
{
    public event EventHandler<bool>? ModalFechado;

    public SimcardModalView()
    {
        InitializeComponent();
    }

    private void Cancelar_Click(object sender, RoutedEventArgs e)
    {
        ModalFechado?.Invoke(this, false);
        Close();
    }

    private async void Salvar_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is ViewModels.SimcardModalViewModel vm)
        {
            var resultado = await vm.SalvarAsync();
            if (resultado)
            {
                ModalFechado?.Invoke(this, true);
                Close();
            }
        }
    }
}
