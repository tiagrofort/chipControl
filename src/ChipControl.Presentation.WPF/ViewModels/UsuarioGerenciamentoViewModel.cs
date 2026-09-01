using ChipControl.Application.DTOs;
using ChipControl.Application.UseCases;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace ChipControl.Presentation.WPF.ViewModels;

public class UsuarioGerenciamentoViewModel : BaseViewModel
{
    private readonly IUsuarioUseCase _useCase;

    private ObservableCollection<UsuarioDto> _usuarios = new();
    private UsuarioDto? _usuarioSelecionado;
    private string _termoBusca = "";
    private bool _isLoading;

    public ObservableCollection<UsuarioDto> Usuarios
    {
        get => _usuarios;
        set => SetProperty(ref _usuarios, value);
    }

    public UsuarioDto? UsuarioSelecionado
    {
        get => _usuarioSelecionado;
        set => SetProperty(ref _usuarioSelecionado, value);
    }

    public string TermoBusca
    {
        get => _termoBusca;
        set
        {
            if (SetProperty(ref _termoBusca, value))
                CollectionViewSource.GetDefaultView(Usuarios)?.Refresh();
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public ICommand RefreshCommand { get; }
    public ICommand NovoCommand { get; }
    public ICommand EditarCommand { get; }
    public ICommand AlternarAtivoCommand { get; }

    public UsuarioGerenciamentoViewModel(IUsuarioUseCase useCase, Action<Exception>? onError = null)
    {
        _useCase = useCase;
        _onError = onError;

        RefreshCommand = new AsyncRelayCommand(CarregarAsync);
        NovoCommand = new RelayCommand(_ => AbrirModalCriacao(), _ => !IsLoading);
        EditarCommand = new RelayCommand(_ => AbrirModalEditar(), _ => !IsLoading && UsuarioSelecionado != null);
        AlternarAtivoCommand = new AsyncRelayCommand(AlternarAtivoAsync, () => !IsLoading && UsuarioSelecionado != null);

        _collectionView = CollectionViewSource.GetDefaultView(Usuarios);
        _collectionView.Filter = Filtrar;
    }

    private readonly Action<Exception>? _onError;
    private readonly ICollectionView _collectionView;

    private bool Filtrar(object? obj)
    {
        if (obj is not UsuarioDto u) return false;
        if (string.IsNullOrWhiteSpace(TermoBusca)) return true;

        return u.Nome.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ||
               u.Login.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ||
               (u.Email?.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ?? false) ||
               u.NivelAcesso.ToString().Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ||
               (u.Observacoes?.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ?? false) ||
               u.Id.ToString().Contains(TermoBusca);
    }

    public async Task CarregarAsync()
    {
        if (IsLoading) return;
        IsLoading = true;
        try
        {
            var lista = await _useCase.ListarAsync();
            Usuarios = new ObservableCollection<UsuarioDto>(lista);
            _collectionView?.Refresh();
        }
        catch (Exception ex)
        {
            _onError?.Invoke(ex);
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task AlternarAtivoAsync()
    {
        if (UsuarioSelecionado == null) return;
        try
        {
            await _useCase.AlternarAtivoAsync(UsuarioSelecionado.Id);
            await CarregarAsync();
        }
        catch (Exception ex)
        {
            _onError?.Invoke(ex);
        }
    }

    private void AbrirModalCriacao() { }
    private void AbrirModalEditar() { }
}
