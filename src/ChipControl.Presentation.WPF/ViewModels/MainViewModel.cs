using ChipControl.Application.DTOs;
using ChipControl.Application.UseCases;
using System.Threading.Tasks;

namespace ChipControl.Presentation.WPF.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly UsuarioAutenticadoDto _usuario;
    private MainView _currentView = MainView.Dashboard;

    public MainViewModel(UsuarioAutenticadoDto usuario)
    {
        _usuario = usuario;
    }

    public UsuarioAutenticadoDto Usuario => _usuario;

    public MainView CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value);
    }

    public void NavegarPara(MainView view)
    {
        CurrentView = view;
    }
}
