using ChipControl.Application.DTOs;
using ChipControl.Application.UseCases;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace ChipControl.Presentation.WPF.ViewModels;

public class SimcardGerenciamentoViewModel : BaseViewModel
{
    private readonly ISimcardUseCase _useCase;
    private readonly Action<Exception>? _onError;
    private readonly ICollectionView _collectionView;

    private ObservableCollection<SimcardDto> _simcards = new();
    private SimcardDto? _simcardSelecionado;
    private string _termoBusca = "";
    private bool _isLoading;

    public ObservableCollection<SimcardDto> Simcards
    {
        get => _simcards;
        set => SetProperty(ref _simcards, value);
    }

    public SimcardDto? SimcardSelecionado
    {
        get => _simcardSelecionado;
        set => SetProperty(ref _simcardSelecionado, value);
    }

    public string TermoBusca
    {
        get => _termoBusca;
        set
        {
            if (SetProperty(ref _termoBusca, value))
                CollectionViewSource.GetDefaultView(Simcards)?.Refresh();
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

    public SimcardGerenciamentoViewModel(ISimcardUseCase useCase, Action<Exception>? onError = null)
    {
        _useCase = useCase;
        _onError = onError;

        RefreshCommand = new AsyncRelayCommand(CarregarAsync);
        NovoCommand = new RelayCommand(_ => AbrirModalCriacao(), _ => !IsLoading);
        EditarCommand = new RelayCommand(_ => AbrirModalEditar(), _ => !IsLoading && SimcardSelecionado != null);
        AlternarAtivoCommand = new AsyncRelayCommand(AlternarAtivoAsync, () => !IsLoading && SimcardSelecionado != null);

        _collectionView = CollectionViewSource.GetDefaultView(Simcards);
        _collectionView.Filter = Filtrar;
    }

    private bool Filtrar(object? obj)
    {
        if (obj is not SimcardDto s) return false;
        if (string.IsNullOrWhiteSpace(TermoBusca)) return true;

        return s.Iccid.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ||
               s.IdentificacaoChip.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ||
               s.OperadoraNome.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ||
               (s.Ddd?.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ?? false) ||
               (s.PlanoTipo?.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ?? false) ||
               s.StatusTexto.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ||
               (s.Observacoes?.Contains(TermoBusca, StringComparison.OrdinalIgnoreCase) ?? false) ||
               s.Id.ToString().Contains(TermoBusca);
    }

    public async Task CarregarAsync()
    {
        if (IsLoading) return;
        IsLoading = true;
        try
        {
            var lista = await _useCase.ListarAsync();
            Simcards = new ObservableCollection<SimcardDto>(lista);
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
        if (SimcardSelecionado == null) return;
        try
        {
            await _useCase.AlternarAtivoAsync(SimcardSelecionado.Id);
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