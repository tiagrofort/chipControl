namespace ChipControl.Application.DTOs;

public class FuncionarioDto
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public string? Matricula { get; set; }
    public string Setor { get; set; } = string.Empty;
    public string? Cargo { get; set; }
    public string? TelefonePessoal { get; set; }
    public string? Email { get; set; }
    public bool Ativo { get; set; }
    public string? Observacoes { get; set; }
}

public class CriarFuncionarioDto
{
    public string NomeCompleto { get; set; } = string.Empty;
    public string Setor { get; set; } = string.Empty;
    public string? Matricula { get; set; }
    public string? Cargo { get; set; }
    public string? TelefonePessoal { get; set; }
    public string? Email { get; set; }
    public bool Ativo { get; set; } = true;
    public string? Observacoes { get; set; }
}

public class EditarFuncionarioDto
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public string Setor { get; set; } = string.Empty;
    public string? Matricula { get; set; }
    public string? Cargo { get; set; }
    public string? TelefonePessoal { get; set; }
    public string? Email { get; set; }
    public bool Ativo { get; set; }
    public string? Observacoes { get; set; }
}
