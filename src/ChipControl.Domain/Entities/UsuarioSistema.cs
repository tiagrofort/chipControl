namespace ChipControl.Domain.Entities;

using ChipControl.Domain.Enums;

public class UsuarioSistema
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Login { get; private set; } = string.Empty;
    public string SenhaHash { get; private set; } = string.Empty;
    public string? Email { get; private set; }
    public NivelAcesso NivelAcesso { get; private set; }
    public bool Ativo { get; private set; }
    public string? Observacoes { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public DateTime? DataAlteracao { get; private set; }

    private UsuarioSistema() { }

    public static UsuarioSistema Create(
        string nome,
        string login,
        string senhaHash,
        NivelAcesso nivelAcesso,
        string? email = null,
        string? observacoes = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(login))
            throw new ArgumentException("Login é obrigatório.", nameof(login));
        if (string.IsNullOrWhiteSpace(senhaHash))
            throw new ArgumentException("Senha é obrigatória.", nameof(senhaHash));

        return new UsuarioSistema
        {
            Nome = nome,
            Login = login,
            SenhaHash = senhaHash,
            Email = email,
            NivelAcesso = nivelAcesso,
            Ativo = true,
            Observacoes = observacoes,
            DataCadastro = DateTime.UtcNow
        };
    }

    internal static UsuarioSistema CreateMaster(string nome)
    {
        return new UsuarioSistema
        {
            Nome = nome,
            Login = "",
            SenhaHash = "",
            NivelAcesso = NivelAcesso.Administrador,
            Ativo = true,
            DataCadastro = DateTime.UtcNow
        };
    }

    public void AlterarSenha(string novaSenhaHash)
    {
        if (string.IsNullOrWhiteSpace(novaSenhaHash))
            throw new ArgumentException("Senha é obrigatória.", nameof(novaSenhaHash));

        SenhaHash = novaSenhaHash;
        DataAlteracao = DateTime.UtcNow;
    }

    public void DefinirInativo()
    {
        Ativo = false;
        DataAlteracao = DateTime.UtcNow;
    }

    public void DefinirAtivo()
    {
        Ativo = true;
        DataAlteracao = DateTime.UtcNow;
    }

    public bool PodeAutenticar() => Ativo;
}
