using ChipControl.Domain.Entities;

namespace ChipControl.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<UsuarioSistema?> BuscarPorLoginAsync(string login);
    Task<UsuarioSistema?> BuscarPorIdAsync(int id);
    Task<bool> ExisteLoginAsync(string login);
    Task<bool> ExisteLoginAsync(string login, int idExato);
    Task AdicionarAsync(UsuarioSistema usuario);
    Task AtualizarAsync(UsuarioSistema usuario);
    Task<IEnumerable<UsuarioSistema>> ListarAsync();
    Task<int> CountAsync();
}
