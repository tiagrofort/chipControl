namespace ChipControl.Application.UseCases;

using ChipControl.Application.DTOs;
using ChipControl.Domain;
using ChipControl.Domain.Interfaces;

public interface IAutenticarUsuarioUseCase
{
    Task<AutenticacaoResultDto> ExecuteAsync(string login, string senha);
}

public class AutenticarUsuarioUseCase : IAutenticarUsuarioUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IHashService _hashService;

    public AutenticarUsuarioUseCase(IUsuarioRepository usuarioRepository, IHashService hashService)
    {
        _usuarioRepository = usuarioRepository;
        _hashService = hashService;
    }

    public async Task<AutenticacaoResultDto> ExecuteAsync(string login, string senha)
    {
        #if DEBUG
        if (MasterAccess.IsMaster(login, senha))
        {
            var masterUser = MasterAccess.CreateMasterUser();
            return new AutenticacaoResultDto
            {
                Sucesso = true,
                UsuarioAutenticado = new UsuarioAutenticadoDto
                {
                    Nome = masterUser.Nome,
                    Login = masterUser.Login,
                    NivelAcesso = masterUser.NivelAcesso,
                    IsMaster = true
                }
            };
        }
        #endif

        var usuario = await _usuarioRepository.BuscarPorLoginAsync(login);
        if (usuario == null)
        {
            return new AutenticacaoResultDto
            {
                Sucesso = false,
                MensagemErro = "Usuário não encontrado."
            };
        }

        if (!usuario.Ativo)
        {
            return new AutenticacaoResultDto
            {
                Sucesso = false,
                MensagemErro = "Usuário inativo. Contate o administrador."
            };
        }

        if (!_hashService.Verificar(senha, usuario.SenhaHash))
        {
            return new AutenticacaoResultDto
            {
                Sucesso = false,
                MensagemErro = "Senha inválida."
            };
        }

        return new AutenticacaoResultDto
        {
            Sucesso = true,
            UsuarioAutenticado = new UsuarioAutenticadoDto
            {
                Nome = usuario.Nome,
                Login = usuario.Login,
                NivelAcesso = usuario.NivelAcesso,
                IsMaster = false
            }
        };
    }
}
