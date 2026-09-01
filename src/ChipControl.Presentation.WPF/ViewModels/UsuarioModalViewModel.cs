using ChipControl.Application.DTOs;
using ChipControl.Application.UseCases;
using ChipControl.Domain.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace ChipControl.Presentation.WPF.ViewModels;

public class UsuarioModalViewModel : BaseViewModel, INotifyDataErrorInfo
{
    private readonly IUsuarioUseCase _useCase;
    private readonly bool _isEditMode;
    private int? _id;
    private string _nome = "";
    private string _login = "";
    private string? _email;
    private NivelAcesso _nivelAcesso = NivelAcesso.Usuario;
    private bool _ativo = true;
    private string? _observacoes;
    private readonly Dictionary<string, string> _errors = new();

    public string Senha { get; set; } = "";
    public string ConfirmarSenha { get; set; } = "";

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

    public string Login
    {
        get => _login;
        set { SetProperty(ref _login, value); ClearErrors(nameof(Login)); }
    }

    public string? Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public NivelAcesso NivelAcesso
    {
        get => _nivelAcesso;
        set => SetProperty(ref _nivelAcesso, value);
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

    public string TituloModal => _isEditMode ? "Editar Usuário" : "Novo Usuário";

    public UsuarioModalViewModel(IUsuarioUseCase useCase)
    {
        _useCase = useCase;
    }

    public UsuarioModalViewModel(IUsuarioUseCase useCase, UsuarioDto usuario) : this(useCase)
    {
        _isEditMode = true;
        Id = usuario.Id;
        Nome = usuario.Nome;
        Login = usuario.Login;
        Email = usuario.Email;
        NivelAcesso = usuario.NivelAcesso;
        Ativo = usuario.Ativo;
        Observacoes = usuario.Observacoes;
    }

    public async Task<bool> SalvarAsync()
    {
        Validate();
        if (HasErrors) return false;

        try
        {
            if (_isEditMode && Id.HasValue)
            {
                await _useCase.EditarAsync(new EditarUsuarioDto
                {
                    Id = Id.Value,
                    Nome = Nome,
                    Login = Login,
                    Senha = string.IsNullOrWhiteSpace(Senha) ? null : Senha,
                    Email = Email,
                    NivelAcesso = NivelAcesso,
                    Ativo = Ativo,
                    Observacoes = Observacoes
                });
            }
            else
            {
                await _useCase.CriarAsync(new CriarUsuarioDto
                {
                    Nome = Nome,
                    Login = Login,
                    Senha = Senha,
                    Email = Email,
                    NivelAcesso = NivelAcesso,
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
            _errors[nameof(Nome)] = "Nome é obrigatório.";
        if (string.IsNullOrWhiteSpace(Login))
            _errors[nameof(Login)] = "Login é obrigatório.";
        if (!_isEditMode)
        {
            if (string.IsNullOrWhiteSpace(Senha))
                _errors[nameof(Senha)] = "Senha é obrigatória.";
            else if (Senha != ConfirmarSenha)
                _errors[nameof(ConfirmarSenha)] = "Senhas não coincidem.";
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(Senha) && Senha != ConfirmarSenha)
                _errors[nameof(ConfirmarSenha)] = "Senhas não coincidem.";
        }

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
