using ChipControl.Application.DTOs;
using ChipControl.Application.UseCases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace ChipControl.Presentation.WPF.ViewModels;

public class FuncionarioModalViewModel : BaseViewModel, INotifyDataErrorInfo
{
    private readonly IFuncionarioUseCase _useCase;
    private readonly bool _isEditMode;
    private int? _id;
    private string _nomeCompleto = "";
    private string _setor = "";
    private string? _matricula;
    private string? _cargo;
    private string? _telefonePessoal;
    private string? _email;
    private bool _ativo = true;
    private string? _observacoes;
    private readonly Dictionary<string, string> _errors = new();

    public int? Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string NomeCompleto
    {
        get => _nomeCompleto;
        set { SetProperty(ref _nomeCompleto, value); ClearErrors(nameof(NomeCompleto)); }
    }

    public string Setor
    {
        get => _setor;
        set { SetProperty(ref _setor, value); ClearErrors(nameof(Setor)); }
    }

    public string? Matricula
    {
        get => _matricula;
        set => SetProperty(ref _matricula, value);
    }

    public string? Cargo
    {
        get => _cargo;
        set => SetProperty(ref _cargo, value);
    }

    public string? TelefonePessoal
    {
        get => _telefonePessoal;
        set => SetProperty(ref _telefonePessoal, value);
    }

    public string? Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
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

    public string TituloModal => _isEditMode ? "Editar Funcionário" : "Novo Funcionário";

    public FuncionarioModalViewModel(IFuncionarioUseCase useCase)
    {
        _useCase = useCase;
    }

    public FuncionarioModalViewModel(IFuncionarioUseCase useCase, FuncionarioDto funcionario) : this(useCase)
    {
        _isEditMode = true;
        Id = funcionario.Id;
        NomeCompleto = funcionario.NomeCompleto;
        Matricula = funcionario.Matricula;
        Setor = funcionario.Setor;
        Cargo = funcionario.Cargo;
        TelefonePessoal = funcionario.TelefonePessoal;
        Email = funcionario.Email;
        Ativo = funcionario.Ativo;
        Observacoes = funcionario.Observacoes;
    }

    public async Task<bool> SalvarAsync()
    {
        Validate();
        if (HasErrors) return false;

        try
        {
            if (_isEditMode && Id.HasValue)
            {
                await _useCase.EditarAsync(new EditarFuncionarioDto
                {
                    Id = Id.Value,
                    NomeCompleto = NomeCompleto,
                    Setor = Setor,
                    Matricula = Matricula,
                    Cargo = Cargo,
                    TelefonePessoal = TelefonePessoal,
                    Email = Email,
                    Ativo = Ativo,
                    Observacoes = Observacoes
                });
            }
            else
            {
                await _useCase.CriarAsync(new CriarFuncionarioDto
                {
                    NomeCompleto = NomeCompleto,
                    Setor = Setor,
                    Matricula = Matricula,
                    Cargo = Cargo,
                    TelefonePessoal = TelefonePessoal,
                    Email = Email,
                    Ativo = Ativo,
                    Observacoes = Observacoes
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

    private void Validate()
    {
        _errors.Clear();

        if (string.IsNullOrWhiteSpace(NomeCompleto))
            _errors[nameof(NomeCompleto)] = "Nome completo é obrigatório.";
        if (string.IsNullOrWhiteSpace(Setor))
            _errors[nameof(Setor)] = "Setor é obrigatório.";

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
