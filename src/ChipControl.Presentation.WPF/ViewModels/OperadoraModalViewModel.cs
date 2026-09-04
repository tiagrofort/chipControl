using ChipControl.Application.DTOs;
using ChipControl.Application.UseCases;
using System;
using System.Collections;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace ChipControl.Presentation.WPF.ViewModels;

public class OperadoraModalViewModel : BaseViewModel, INotifyDataErrorInfo
{
    private readonly IOperadoraUseCase _useCase;
    private readonly bool _isEditMode;
    private int? _id;
    private string _nome = "";
    private string? _codigo;
    private string? _cnpj;
    private string? _telefone;
    private string? _email;
    private bool _ativo = true;
    private string? _observacoes;
    private readonly Dictionary<string, string> _errors = new();

    public int? Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Nome
    {
        get => _nome;
        set { SetProperty(ref _nome, value); ClearErrors(nameof(Nome)); }
    }

    public string? Codigo
    {
        get => _codigo;
        set => SetProperty(ref _codigo, value);
    }

    public string? Cnpj
    {
        get => _cnpj;
        set => SetProperty(ref _cnpj, value);
    }

    public string? Telefone
    {
        get => _telefone;
        set => SetProperty(ref _telefone, value);
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

    public string TituloModal => _isEditMode ? "Editar Operadora" : "Nova Operadora";

    public OperadoraModalViewModel(IOperadoraUseCase useCase)
    {
        _useCase = useCase;
    }

    public OperadoraModalViewModel(IOperadoraUseCase useCase, OperadoraDto operadora) : this(useCase)
    {
        _isEditMode = true;
        Id = operadora.Id;
        Nome = operadora.Nome;
        Codigo = operadora.Codigo;
        Cnpj = operadora.Cnpj;
        Telefone = operadora.Telefone;
        Email = operadora.Email;
        Ativo = operadora.Ativo;
        Observacoes = operadora.Observacoes;
    }

    public async Task<bool> SalvarAsync()
    {
        Validate();
        if (HasErrors) return false;

        try
        {
            if (_isEditMode && Id.HasValue)
            {
                await _useCase.EditarAsync(new EditarOperadoraDto
                {
                    Id = Id.Value,
                    Nome = Nome,
                    Codigo = Codigo,
                    Cnpj = Cnpj,
                    Telefone = Telefone,
                    Email = Email,
                    Ativo = Ativo,
                    Observacoes = Observacoes
                });
            }
            else
            {
                await _useCase.CriarAsync(new CriarOperadoraDto
                {
                    Nome = Nome,
                    Codigo = Codigo,
                    Cnpj = Cnpj,
                    Telefone = Telefone,
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

        if (string.IsNullOrWhiteSpace(Nome))
            _errors[nameof(Nome)] = "Nome da operadora é obrigatório.";
        else if (Nome.Trim().Length > 100)
            _errors[nameof(Nome)] = "Nome deve ter no máximo 100 caracteres.";

        if (!string.IsNullOrWhiteSpace(Codigo) && Codigo.Trim().Length > 20)
            _errors[nameof(Codigo)] = "Código deve ter no máximo 20 caracteres.";

        if (!string.IsNullOrWhiteSpace(Cnpj) && Cnpj.Trim().Length > 20)
            _errors[nameof(Cnpj)] = "CNPJ deve ter no máximo 20 caracteres.";

        if (!string.IsNullOrWhiteSpace(Telefone) && Telefone.Trim().Length > 30)
            _errors[nameof(Telefone)] = "Telefone deve ter no máximo 30 caracteres.";

        if (!string.IsNullOrWhiteSpace(Email) && Email.Trim().Length > 255)
            _errors[nameof(Email)] = "E-mail deve ter no máximo 255 caracteres.";

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