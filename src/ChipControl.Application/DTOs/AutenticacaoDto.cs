namespace ChipControl.Application.DTOs;

using ChipControl.Domain.Enums;

public class UsuarioAutenticadoDto
{
    public string Nome { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public NivelAcesso NivelAcesso { get; set; }
    public bool IsMaster { get; set; }
}

public class AutenticacaoResultDto
{
    public bool Sucesso { get; set; }
    public string MensagemErro { get; set; } = string.Empty;
    public UsuarioAutenticadoDto? UsuarioAutenticado { get; set; }
}
