namespace ChipControl.Application.DTOs;

public class OperadoraDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Codigo { get; set; }
    public string? Cnpj { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public bool Ativo { get; set; }
    public string? Observacoes { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAlteracao { get; set; }
}

public class CriarOperadoraDto
{
    public string Nome { get; set; } = string.Empty;
    public string? Codigo { get; set; }
    public string? Cnpj { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public string? Observacoes { get; set; }
    public bool Ativo { get; set; } = true;
}

public class EditarOperadoraDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Codigo { get; set; }
    public string? Cnpj { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public string? Observacoes { get; set; }
    public bool Ativo { get; set; }
}