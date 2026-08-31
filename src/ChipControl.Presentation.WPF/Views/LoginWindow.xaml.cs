using ChipControl.Application.UseCases;
using System;
using System.Windows;

namespace ChipControl.Presentation.WPF.Views;

public partial class LoginWindow : Window
{
    private readonly IAutenticarUsuarioUseCase _authUseCase;

    public LoginWindow(IAutenticarUsuarioUseCase authUseCase)
    {
        InitializeComponent();
        _authUseCase = authUseCase;
    }

    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        var usuario = UsernameTextBox.Text;
        var senha = PasswordBox.Password;

        if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(senha))
        {
            MessageBox.Show("Informe usuario e senha.", "Login", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            var result = await _authUseCase.ExecuteAsync(usuario, senha);

            if (result.Sucesso && result.UsuarioAutenticado != null)
            {
                var mainWindow = new MainWindow(result.UsuarioAutenticado);
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show(result.MensagemErro ?? "Falha na autenticacao.", "Login", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
