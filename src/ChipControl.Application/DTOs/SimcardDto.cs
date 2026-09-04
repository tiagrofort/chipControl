namespace ChipControl.Application.DTOs;

using ChipControl.Domain.Enums;

public class SimcardDto
{
    public int Id { get; set; }
    public int OperadoraId { get; set; }
    public string OperadoraNome { get; set; } = string.Empty;
    public string IdentificacaoChip { get; set; } = string.Empty;
    public string Iccid { get; set; } = string.Empty;
    public string? Ddd { get; set; }
    public string? PlanoTipo { get; set; }
    public bool TemMinutagem { get; set; }
    public int? QuantidadeMinutos { get; set; }
    public bool TemInternet { get; set; }
    public int? QuantidadeInternet { get; set; }
    public DateTime? DataAquisicao { get; set; }
    public DateTime? DataAtivacao { get; set; }
    public SimcardStatus Status { get; set; }
    public string StatusTexto { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public string? Observacoes { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAlteracao { get; set; }
}

public class CriarSimcardDto
{
    public int OperadoraId { get; set; }
    public string IdentificacaoChip { get; set; } = string.Empty;
    public string Iccid { get; set; } = string.Empty;
    public string? Ddd { get; set; }
    public string? PlanoTipo { get; set; }
    public bool TemMinutagem { get; set; }
    public int? QuantidadeMinutos { get; set; }
    public bool TemInternet { get; set; }
    public int? QuantidadeInternet { get; set; }
    public DateTime? DataAquisicao { get; set; }
    public DateTime? DataAtivacao { get; set; }
    public string? Observacoes { get; set; }
    public bool Ativo { get; set; } = true;
}

public class EditarSimcardDto
{
    public int Id { get; set; }
    public int OperadoraId { get; set; }
    public string IdentificacaoChip { get; set; } = string.Empty;
    public string Iccid { get; set; } = string.Empty;
    public string? Ddd { get; set; }
    public string? PlanoTipo { get; set; }
    public bool TemMinutagem { get; set; }
    public int? QuantidadeMinutos { get; set; }
    public bool TemInternet { get; set; }
    public int? QuantidadeInternet { get; set; }
    public DateTime? DataAquisicao { get; set; }
    public DateTime? DataAtivacao { get; set; }
    public string? Observacoes { get; set; }
    public bool Ativo { get; set; }
}

/// <summary>Operadora mínima para seleção no cadastro de SIMCARDs.</summary>
public class OperadoraDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
}