namespace ChipControl.Domain.Entities;

/// <summary>
/// Operadora de telefonia (modelo de dados, seção 3).
/// Entidade mínima para referenciamento pelos SIMCARDs; o módulo
/// completo de Operadoras será tratado em prompt próprio.
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

    private Operadora() { }

    public static Operadora Create(string nome, bool ativo = true)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome da operadora é obrigatório.", nameof(nome));

        return new Operadora
        {
            Nome = nome,
            Ativo = ativo,
            DataCadastro = DateTime.Now
        };
    }

    public void DefinirInativo()
    {
        Ativo = false;
    }

    public void DefinirAtivo()
    {
        Ativo = true;
    }
}
