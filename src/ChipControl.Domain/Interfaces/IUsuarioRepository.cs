namespace ChipControl.Domain.Interfaces;

using ChipControl.Domain.Entities;

public interface IUsuarioRepository
{
    Task<UsuarioSistema?> BuscarPorLoginAsync(string login);
    Task<bool> ExisteLoginAsync(string login);
    Task AdicionarAsync(UsuarioSistema usuario);
    Task<int> CountAsync();
}
