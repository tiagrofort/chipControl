using System;
using System.Windows.Input;

namespace ChipControl.Presentation.WPF.ViewModels;

public class LoginViewModel : BaseViewModel
{
    private string _usuario = "";
    private string _senha = "";
    private bool _isLoading;

    public string Usuario
    {
        get => _usuario;
        set => SetProperty(ref _usuario, value);
    }

    public string Senha
    {
        get => _senha;
        set => SetProperty(ref _senha, value);
    }

    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public ICommand? EntrarCommand { get; set; }
}
