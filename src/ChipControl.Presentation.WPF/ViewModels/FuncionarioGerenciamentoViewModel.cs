using ChipControl.Application.DTOs;
using ChipControl.Application.UseCases;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace ChipControl.Presentation.WPF.ViewModels;

public class FuncionarioGerenciamentoViewModel : BaseViewModel
{
    private readonly IFuncionarioUseCase _useCase;
    private readonly Action<Exception>? _onError;
    private readonly ICollectionView _collectionView;

    private ObservableCollection<FuncionarioDto> _funcionarios = new();
    private FuncionarioDto? _funcionarioSelecionado;
    private string _termoBusca = "";
    private bool _isLoading;

    public ObservableCollection<FuncionarioDto> Funcionarios
    {
        get => _funcionarios;
        set => SetProperty(ref _funcionarios, value);
    }

    public FuncionarioDto? FuncionarioSelecionado
    {
        get => _funcionarioSelecionado;
        set => SetProperty(ref _funcionarioSelecionado, value);
    }

    public string TermoBusca
    {
        get => _termoBusca;
        set
        {
            if (SetProperty(ref _termoBusca, value))
                CollectionViewSource.GetDefaultView(Funcionarios)?.Refresh();
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

    public FuncionarioGerenciamentoViewModel(IFuncionarioUseCase useCase, Action<Exception>? onError = null)
    {
        _useCase = useCase;
        _onError = onError;

        RefreshCommand = new AsyncRelayCommand(CarregarAsync);
        NovoCommand = new RelayCommand(_ => AbrirModalCriacao(), _ => !IsLoading);
        EditarCommand = new RelayCommand(_ => AbrirModalEditar(), _ => !IsLoading && FuncionarioSelecionado != null);
        AlternarAtivoCommand = new AsyncRelayCommand(AlternarAtivoAsync, () => !IsLoading && FuncionarioSelecionado != null);

        _collectionView = CollectionViewSource.GetDefaultView(Funcionarios);
        _collectionView.Filter = Filtrar;
    }

    private bool Filtrar(object? obj)
    {
        if (obj is not FuncionarioDto f) return false;
        if (string.IsNullOrWhiteSpace(TermoBusca)) return true;

        return f.NomeCompleto.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ||
               (f.Matricula?.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ?? false) ||
               f.Setor.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ||
               (f.Cargo?.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ?? false) ||
               (f.TelefonePessoal?.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ?? false) ||
               (f.Email?.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ?? false) ||
               (f.Observacoes?.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ?? false) ||
               f.Id.ToString().Contains(TermoBusca);
    }

    public async Task CarregarAsync()
    {
        if (IsLoading) return;
        IsLoading = true;
        try
        {
            var lista = await _useCase.ListarAsync();
            Funcionarios = new ObservableCollection<FuncionarioDto>(lista);
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
        if (FuncionarioSelecionado == null) return;
        try
        {
            await _useCase.AlternarAtivoAsync(FuncionarioSelecionado.Id);
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
