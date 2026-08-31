namespace ChipControl.Application.UseCases;

using ChipControl.Domain;
using ChipControl.Domain.Entities;
using ChipControl.Domain.Enums;
using ChipControl.Domain.Interfaces;

public interface ICriarAdministradorInicialUseCase
{
    Task<bool> ExecuteAsync(string nome, string login, string senha, string? email = null);
}

public class CriarAdministradorInicialUseCase : ICriarAdministradorInicialUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IHashService _hashService;

    public CriarAdministradorInicialUseCase(IUsuarioRepository usuarioRepository, IHashService hashService)
    {
        _usuarioRepository = usuarioRepository;
        _hashService = hashService;
    }

    public async Task<bool> ExecuteAsync(string nome, string login, string senha, string? email = null)
    {
        var exists = await _usuarioRepository.ExisteLoginAsync(login);
        if (exists)
            return false;

        var senhaHash = _hashService.Hash(senha);
        var usuario = UsuarioSistema.Create(nome, login, senhaHash, NivelAcesso.Administrador, email);
        await _usuarioRepository.AdicionarAsync(usuario);

        return true;
    }
}
