using ChipControl.Domain.Enums;

namespace ChipControl.Application.DTOs;

public class UsuarioDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string? Email { get; set; }
    public NivelAcesso NivelAcesso { get; set; }
    public bool Ativo { get; set; }
    public string? Observacoes { get; set; }
}

public class CriarUsuarioDto
{
    public string Nome { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string? Email { get; set; }
    public NivelAcesso NivelAcesso { get; set; } = NivelAcesso.Usuario;
    public bool Ativo { get; set; } = true;
    public string? Observacoes { get; set; }
}

public class EditarUsuarioDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string? Senha { get; set; }
    public string? Email { get; set; }
    public NivelAcesso NivelAcesso { get; set; }
    public bool Ativo { get; set; }
    public string? Observacoes { get; set; }
}
