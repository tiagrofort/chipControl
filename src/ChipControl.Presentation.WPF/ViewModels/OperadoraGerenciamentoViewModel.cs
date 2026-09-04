using ChipControl.Application.DTOs;
using ChipControl.Application.UseCases;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace ChipControl.Presentation.WPF.ViewModels;

public class OperadoraGerenciamentoViewModel : BaseViewModel
{
    private readonly IOperadoraUseCase _useCase;
    private readonly Action<Exception>? _onError;
    private readonly ICollectionView _collectionView;

    private ObservableCollection<OperadoraDto> _operadoras = new();
    private OperadoraDto? _operadoraSelecionada;
    private string _termoBusca = "";
    private bool _isLoading;

    public ObservableCollection<OperadoraDto> Operadoras
    {
        get => _operadoras;
        set => SetProperty(ref _operadoras, value);
    }

    public OperadoraDto? OperadoraSelecionada
    {
        get => _operadoraSelecionada;
        set => SetProperty(ref _operadoraSelecionada, value);
    }

    public string TermoBusca
    {
        get => _termoBusca;
        set
        {
            if (SetProperty(ref _termoBusca, value))
                CollectionViewSource.GetDefaultView(Operadoras)?.Refresh();
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

    public OperadoraGerenciamentoViewModel(IOperadoraUseCase useCase, Action<Exception>? onError = null)
    {
        _useCase = useCase;
        _onError = onError;

        RefreshCommand = new AsyncRelayCommand(CarregarAsync);
        NovoCommand = new RelayCommand(_ => AbrirModalCriacao(), _ => !IsLoading);
        EditarCommand = new RelayCommand(_ => AbrirModalEditar(), _ => !IsLoading && OperadoraSelecionada != null);
        AlternarAtivoCommand = new AsyncRelayCommand(AlternarAtivoAsync, () => !IsLoading && OperadoraSelecionada != null);

        _collectionView = CollectionViewSource.GetDefaultView(Operadoras);
        _collectionView.Filter = Filtrar;
    }

    private bool Filtrar(object? obj)
    {
        if (obj is not OperadoraDto o) return false;
        if (string.IsNullOrWhiteSpace(TermoBusca)) return true;

        return o.Nome.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ||
               (o.Codigo?.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ?? false) ||
               (o.Cnpj?.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ?? false) ||
               (o.Telefone?.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ?? false) ||
               (o.Email?.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ?? false) ||
               (o.Observacoes?.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ?? false) ||
               o.Id.ToString().Contains(TermoBusca);
    }

    public async Task CarregarAsync()
    {
        if (IsLoading) return;
        IsLoading = true;
        try
        {
            var lista = await _useCase.ListarAsync();
            Operadoras = new ObservableCollection<OperadoraDto>(lista);
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
        if (OperadoraSelecionada == null) return;
        try
        {
            await _useCase.AlternarAtivoAsync(OperadoraSelecionada.Id);
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