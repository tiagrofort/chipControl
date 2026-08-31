using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ChipControl.Presentation.WPF.ViewModels;

public class NavItemViewModel : BaseViewModel
{
    public string Titulo { get; }
    public string Icone { get; }
    public string Destino { get; }
    private bool _isSelected;

    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    public NavItemViewModel(string titulo, string icone, string destino)
    {
        Titulo = titulo;
        Icone = icone;
        Destino = destino;
    }
}

public enum MainView
{
    Dashboard,
    Funcionarios,
    Operadoras,
    Simcards,
    Aparelhos,
    TrocaNumeros,
    Substituicao,
    Historico,
    Relatorios
}
