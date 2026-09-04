namespace ChipControl.Domain.Entities;

/// <summary>
/// Operadora de telefonia (modelo de dados, seção 3).
/// Regras documentadas (docs/03-MODELO-DE-DADOS.md seção 3 / Prompt 005):
/// - Nome obrigatório;
/// - Código, CNPJ, Telefone, E-mail e Observações opcionais;
/// - Ativo obrigatório;
/// - Sem exclusão física — desligamento pelo campo Ativo.
/// </summary>
public class Operadora
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string? Codigo { get; private set; }
    public string? Cnpj { get; private set; }
    public string? Telefone { get; private set; }
    public string? Email { get; private set; }
    public bool Ativo { get; private set; }
    public string? Observacoes { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public DateTime? DataAlteracao { get; private set; }

    private Operadora() { }

    public static Operadora Create(
        string nome,
        string? codigo = null,
        string? cnpj = null,
        string? telefone = null,
        string? email = null,
        string? observacoes = null,
        bool ativo = true)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome da operadora é obrigatório.", nameof(nome));

        if (nome.Trim().Length > 100)
            throw new ArgumentException("Nome da operadora deve ter no máximo 100 caracteres.", nameof(nome));

        if (!string.IsNullOrWhiteSpace(codigo) && codigo.Trim().Length > 20)
            throw new ArgumentException("Código deve ter no máximo 20 caracteres.", nameof(codigo));

        if (!string.IsNullOrWhiteSpace(cnpj) && cnpj.Trim().Length > 20)
            throw new ArgumentException("CNPJ deve ter no máximo 20 caracteres.", nameof(cnpj));

        if (!string.IsNullOrWhiteSpace(telefone) && telefone.Trim().Length > 30)
            throw new ArgumentException("Telefone deve ter no máximo 30 caracteres.", nameof(telefone));

        if (!string.IsNullOrWhiteSpace(email) && email.Trim().Length > 255)
            throw new ArgumentException("E-mail deve ter no máximo 255 caracteres.", nameof(email));

        return new Operadora
        {
            Nome = nome.Trim(),
            Codigo = string.IsNullOrWhiteSpace(codigo) ? null : codigo.Trim(),
            Cnpj = string.IsNullOrWhiteSpace(cnpj) ? null : cnpj.Trim(),
            Telefone = string.IsNullOrWhiteSpace(telefone) ? null : telefone.Trim(),
            Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim(),
            Observacoes = string.IsNullOrWhiteSpace(observacoes) ? null : observacoes.Trim(),
            Ativo = ativo,
            DataCadastro = DateTime.Now
        };
    }

    public void AtualizarDados(
        string nome,
        string? codigo,
        string? cnpj,
        string? telefone,
        string? email,
        string? observacoes,
        bool ativo)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome da operadora é obrigatório.", nameof(nome));

        if (nome.Trim().Length > 100)
            throw new ArgumentException("Nome da operadora deve ter no máximo 100 caracteres.", nameof(nome));

        if (!string.IsNullOrWhiteSpace(codigo) && codigo.Trim().Length > 20)
            throw new ArgumentException("Código deve ter no máximo 20 caracteres.", nameof(codigo));

        if (!string.IsNullOrWhiteSpace(cnpj) && cnpj.Trim().Length > 20)
            throw new ArgumentException("CNPJ deve ter no máximo 20 caracteres.", nameof(cnpj));

        if (!string.IsNullOrWhiteSpace(telefone) && telefone.Trim().Length > 30)
            throw new ArgumentException("Telefone deve ter no máximo 30 caracteres.", nameof(telefone));

        if (!string.IsNullOrWhiteSpace(email) && email.Trim().Length > 255)
            throw new ArgumentException("E-mail deve ter no máximo 255 caracteres.", nameof(email));

        Nome = nome.Trim();
        Codigo = string.IsNullOrWhiteSpace(codigo) ? null : codigo.Trim();
        Cnpj = string.IsNullOrWhiteSpace(cnpj) ? null : cnpj.Trim();
        Telefone = string.IsNullOrWhiteSpace(telefone) ? null : telefone.Trim();
        Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
        Observacoes = string.IsNullOrWhiteSpace(observacoes) ? null : observacoes.Trim();
        Ativo = ativo;
        DataAlteracao = DateTime.Now;
    }

    public void DefinirInativo()
    {
        Ativo = false;
        DataAlteracao = DateTime.Now;
    }

    public void DefinirAtivo()
    {
        Ativo = true;
        DataAlteracao = DateTime.Now;
    }
}
