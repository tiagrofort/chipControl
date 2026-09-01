using ChipControl.Application.DTOs;
using ChipControl.Domain.Entities;
using ChipControl.Domain.Enums;
using ChipControl.Domain.Interfaces;

namespace ChipControl.Application.UseCases;

public interface IUsuarioUseCase
{
    Task<IEnumerable<UsuarioDto>> ListarAsync();
    Task<UsuarioDto?> BuscarPorIdAsync(int id);
    Task<bool> CriarAsync(CriarUsuarioDto dto);
    Task<bool> EditarAsync(EditarUsuarioDto dto);
    Task<bool> AlternarAtivoAsync(int id);
    Task<IEnumerable<UsuarioDto>> PesquisarAsync(string termo);
}

public class UsuarioUseCase : IUsuarioUseCase
{
    private readonly IUsuarioRepository _repository;
    private readonly IHashService _hashService;

    public UsuarioUseCase(IUsuarioRepository repository, IHashService hashService)
    {
        _repository = repository;
        _hashService = hashService;
    }

    public async Task<IEnumerable<UsuarioDto>> ListarAsync()
    {
        var usuarios = await _repository.ListarAsync();
        return usuarios.Select(MapToDto);
    }

    public async Task<UsuarioDto?> BuscarPorIdAsync(int id)
    {
        var usuario = await _repository.BuscarPorIdAsync(id);
        return usuario != null ? MapToDto(usuario) : null;
    }

    public async Task<bool> CriarAsync(CriarUsuarioDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nome))
            throw new ArgumentException("Nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(dto.Login))
            throw new ArgumentException("Login é obrigatório.");
        if (string.IsNullOrWhiteSpace(dto.Senha))
            throw new ArgumentException("Senha é obrigatória.");
        if (await _repository.ExisteLoginAsync(dto.Login))
            throw new InvalidOperationException("Já existe um usuário com este login.");

        var senhaHash = _hashService.Hash(dto.Senha);
        var usuario = UsuarioSistema.Create(
            dto.Nome, dto.Login, senhaHash, dto.NivelAcesso, dto.Email, dto.Observacoes);

        await _repository.AdicionarAsync(usuario);
        return true;
    }

    public async Task<bool> EditarAsync(EditarUsuarioDto dto)
    {
        var usuario = await _repository.BuscarPorIdAsync(dto.Id);
        if (usuario == null)
            throw new InvalidOperationException("Usuário não encontrado.");

        if (string.IsNullOrWhiteSpace(dto.Nome))
            throw new ArgumentException("Nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(dto.Login))
            throw new ArgumentException("Login é obrigatório.");
        if (await _repository.ExisteLoginAsync(dto.Login, dto.Id))
            throw new InvalidOperationException("Já existe um usuário com este login.");

        if (!string.IsNullOrWhiteSpace(dto.Senha))
        {
            var novaSenhaHash = _hashService.Hash(dto.Senha);
            usuario.AlterarSenha(novaSenhaHash);
        }

        usuario.AtualizarDados(dto.Nome, dto.Login, dto.Email, dto.NivelAcesso, dto.Ativo, dto.Observacoes);

        await _repository.AtualizarAsync(usuario);
        return true;
    }

    public async Task<bool> AlternarAtivoAsync(int id)
    {
        var usuario = await _repository.BuscarPorIdAsync(id);
        if (usuario == null)
            throw new InvalidOperationException("Usuário não encontrado.");

        if (usuario.Ativo)
            usuario.DefinirInativo();
        else
            usuario.DefinirAtivo();

        await _repository.AtualizarAsync(usuario);
        return true;
    }

    public async Task<IEnumerable<UsuarioDto>> PesquisarAsync(string termo)
    {
        var usuarios = await _repository.ListarAsync();
        if (string.IsNullOrWhiteSpace(termo))
            return usuarios.Select(MapToDto);

        var termoLower = termo.ToLowerInvariant();
        return usuarios
            .Where(u =>
                u.Nome.Contains(termoLower, StringComparison.OrdinalIgnoreCase) ||
                u.Login.Contains(termoLower, StringComparison.OrdinalIgnoreCase) ||
                (u.Email != null && u.Email.Contains(termoLower, StringComparison.OrdinalIgnoreCase)) ||
                u.NivelAcesso.ToString().Contains(termoLower, StringComparison.OrdinalIgnoreCase) ||
                u.Observacoes != null && u.Observacoes.Contains(termoLower, StringComparison.OrdinalIgnoreCase) ||
                u.Id.ToString().Contains(termo))
            .Select(MapToDto);
    }

    private static UsuarioDto MapToDto(UsuarioSistema usuario) => new()
    {
        Id = usuario.Id,
        Nome = usuario.Nome,
        Login = usuario.Login,
        Email = usuario.Email,
        NivelAcesso = usuario.NivelAcesso,
        Ativo = usuario.Ativo,
        Observacoes = usuario.Observacoes
    };
}
