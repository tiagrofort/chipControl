namespace ChipControl.Domain.Entities;

public class Funcionario
{
    public int Id { get; private set; }
    public string NomeCompleto { get; private set; } = string.Empty;
    public string? Matricula { get; private set; }
    public string Setor { get; private set; } = string.Empty;
    public string? Cargo { get; private set; }
    public string? TelefonePessoal { get; private set; }
    public string? Email { get; private set; }
    public bool Ativo { get; private set; }
    public string? Observacoes { get; private set; }

    private Funcionario() { }

    public static Funcionario Create(
        string nomeCompleto,
        string setor,
        string? matricula = null,
        string? cargo = null,
        string? telefonePessoal = null,
        string? email = null,
        bool ativo = true,
        string? observacoes = null)
    {
        if (string.IsNullOrWhiteSpace(nomeCompleto))
            throw new ArgumentException("Nome completo é obrigatório.", nameof(nomeCompleto));
        if (string.IsNullOrWhiteSpace(setor))
            throw new ArgumentException("Setor é obrigatório.", nameof(setor));

        return new Funcionario
        {
            NomeCompleto = nomeCompleto,
            Matricula = matricula,
            Setor = setor,
            Cargo = cargo,
            TelefonePessoal = telefonePessoal,
            Email = email,
            Ativo = ativo,
            Observacoes = observacoes
        };
    }

    public void AtualizarDados(
        string nomeCompleto,
        string setor,
        string? matricula,
        string? cargo,
        string? telefonePessoal,
        string? email,
        bool ativo,
        string? observacoes)
    {
        if (string.IsNullOrWhiteSpace(nomeCompleto))
            throw new ArgumentException("Nome completo é obrigatório.", nameof(nomeCompleto));
        if (string.IsNullOrWhiteSpace(setor))
            throw new ArgumentException("Setor é obrigatório.", nameof(setor));

        NomeCompleto = nomeCompleto;
        Setor = setor;
        Matricula = matricula;
        Cargo = cargo;
        TelefonePessoal = telefonePessoal;
        Email = email;
        Ativo = ativo;
        Observacoes = observacoes;
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
