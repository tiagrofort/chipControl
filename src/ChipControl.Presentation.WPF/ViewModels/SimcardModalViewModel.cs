using ChipControl.Application.DTOs;
using ChipControl.Application.UseCases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ChipControl.Presentation.WPF.ViewModels;

public class SimcardModalViewModel : BaseViewModel, INotifyDataErrorInfo
{
    private readonly ISimcardUseCase _useCase;
    private readonly bool _isEditMode;
    private int? _id;
    private int _operadoraId;
    private ObservableCollection<OperadoraDto> _operadoras = new();
    private string _identificacaoChip = "";
    private string _iccid = "";
    private string? _ddd;
    private string? _planoTipo;
    private bool _temMinutagem;
    private string? _quantidadeMinutosTexto;
    private bool _temInternet;
    private string? _quantidadeInternetTexto;
    private DateTime? _dataAquisicao;
    private DateTime? _dataAtivacao;
    private bool _ativo = true;
    private string? _observacoes;
    private readonly Dictionary<string, string> _errors = new();

    public int? Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public ObservableCollection<OperadoraDto> Operadoras
    {
        get => _operadoras;
        set => SetProperty(ref _operadoras, value);
    }

    public OperadoraDto? OperadoraSelecionada
    {
        get => _operadorasEscolhida;
        set
        {
            _operadorasEscolhida = value;
            if (value != null) OperadoraId = value.Id;
            OnPropertyChanged();
        }
    }
    private OperadoraDto? _operadorasEscolhida;

    public int OperadoraId
    {
        get => _operadoraId;
        set => SetProperty(ref _operadoraId, value);
    }

    public string IdentificacaoChip
    {
        get => _identificacaoChip;
        set { SetProperty(ref _identificacaoChip, value); ClearErrors(nameof(IdentificacaoChip)); }
    }

    public string Iccid
    {
        get => _iccid;
        set { SetProperty(ref _iccid, value); ClearErrors(nameof(Iccid)); }
    }

    public string? Ddd
    {
        get => _ddd;
        set => SetProperty(ref _ddd, value);
    }

    public string? PlanoTipo
    {
        get => _planoTipo;
        set => SetProperty(ref _planoTipo, value);
    }

    public bool TemMinutagem
    {
        get => _temMinutagem;
        set => SetProperty(ref _temMinutagem, value);
    }

    public string? QuantidadeMinutosTexto
    {
        get => _quantidadeMinutosTexto;
        set => SetProperty(ref _quantidadeMinutosTexto, value);
    }

    public bool TemInternet
    {
        get => _temInternet;
        set => SetProperty(ref _temInternet, value);
    }

    public string? QuantidadeInternetTexto
    {
        get => _quantidadeInternetTexto;
        set => SetProperty(ref _quantidadeInternetTexto, value);
    }

    public DateTime? DataAquisicao
    {
        get => _dataAquisicao;
        set => SetProperty(ref _dataAquisicao, value);
    }

    public DateTime? DataAtivacao
    {
        get => _dataAtivacao;
        set => SetProperty(ref _dataAtivacao, value);
    }

    public bool Ativo
    {
        get => _ativo;
        set => SetProperty(ref _ativo, value);
    }

    public string? Observacoes
    {
        get => _observacoes;
        set => SetProperty(ref _observacoes, value);
    }

    public string TituloModal => _isEditMode ? "Editar SIMCARD" : "Novo SIMCARD";

    public SimcardModalViewModel(ISimcardUseCase useCase)
    {
        _useCase = useCase;
    }

    public SimcardModalViewModel(ISimcardUseCase useCase, SimcardDto simcard) : this(useCase)
    {
        _isEditMode = true;
        Id = simcard.Id;
        OperadoraId = simcard.OperadoraId;
        IdentificacaoChip = simcard.IdentificacaoChip;
        Iccid = simcard.Iccid;
        Ddd = simcard.Ddd;
        PlanoTipo = simcard.PlanoTipo;
        TemMinutagem = simcard.TemMinutagem;
        QuantidadeMinutosTexto = simcard.QuantidadeMinutos?.ToString();
        TemInternet = simcard.TemInternet;
        QuantidadeInternetTexto = simcard.QuantidadeInternet?.ToString();
        DataAquisicao = simcard.DataAquisicao;
        DataAtivacao = simcard.DataAtivacao;
        Ativo = simcard.Ativo;
        Observacoes = simcard.Observacoes;
    }

    /// <summary>Carrega as operadoras para o combo e seleciona a atual (modo edicao).</summary>
    public async Task CarregarOperadorasAsync()
    {
        try
        {
            var lista = await _useCase.ListarOperadorasAsync();
            Operadoras = new ObservableCollection<OperadoraDto>(lista);
            OperadoraSelecionada = _isEditMode
                ? Operadoras.FirstOrDefault(o => o.Id == OperadoraId)
                : Operadoras.FirstOrDefault();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public async Task<bool> SalvarAsync()
    {
        Validate();
        if (HasErrors) return false;

        try
        {
            if (_isEditMode && Id.HasValue)
            {
                await _useCase.EditarAsync(new EditarSimcardDto
                {
                    Id = Id.Value,
                    OperadoraId = OperadoraId,
                    IdentificacaoChip = IdentificacaoChip.Trim(),
                    Iccid = Iccid.Trim(),
                    Ddd = Ddd,
                    PlanoTipo = PlanoTipo,
                    TemMinutagem = TemMinutagem,
                    QuantidadeMinutos = TemMinutagem ? ParseInt(QuantidadeMinutosTexto) : null,
                    TemInternet = TemInternet,
                    QuantidadeInternet = TemInternet ? ParseInt(QuantidadeInternetTexto) : null,
                    DataAquisicao = DataAquisicao,
                    DataAtivacao = DataAtivacao,
                    Observacoes = Observacoes,
                    Ativo = Ativo
                });
            }
            else
            {
                await _useCase.CriarAsync(new CriarSimcardDto
                {
                    OperadoraId = OperadoraId,
                    IdentificacaoChip = IdentificacaoChip.Trim(),
                    Iccid = Iccid.Trim(),
                    Ddd = Ddd,
                    PlanoTipo = PlanoTipo,
                    TemMinutagem = TemMinutagem,
                    QuantidadeMinutos = TemMinutagem ? ParseInt(QuantidadeMinutosTexto) : null,
                    TemInternet = TemInternet,
                    QuantidadeInternet = TemInternet ? ParseInt(QuantidadeInternetTexto) : null,
                    DataAquisicao = DataAquisicao,
                    DataAtivacao = DataAtivacao,
                    Observacoes = Observacoes,
                    Ativo = Ativo
                });
            }
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
    }

    private static int? ParseInt(string? texto)
        => int.TryParse(texto?.Trim(), out var valor) ? valor : null;

    private void Validate()
    {
        _errors.Clear();

        if (OperadoraId <= 0)
            _errors[nameof(OperadoraSelecionada)] = "Selecione a operadora.";
        if (string.IsNullOrWhiteSpace(IdentificacaoChip))
            _errors[nameof(IdentificacaoChip)] = "Identificação do chip é obrigatória.";
        if (string.IsNullOrWhiteSpace(Iccid))
            _errors[nameof(Iccid)] = "ICCID é obrigatório.";
        if (TemMinutagem && ParseInt(QuantidadeMinutosTexto) is null)
            _errors[nameof(QuantidadeMinutosTexto)] = "Informe a quantidade de minutos.";
        if (TemInternet && ParseInt(QuantidadeInternetTexto) is null)
            _errors[nameof(QuantidadeInternetTexto)] = "Informe a quantidade de internet.";

        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(null));
    }

    private void ClearErrors(string propertyName)
    {
        _errors.Remove(propertyName);
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    public bool HasErrors => _errors.Count > 0;
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName == null) return _errors.Values;
        return _errors.TryGetValue(propertyName, out var error) ? new[] { error } : Array.Empty<string>();
    }
}