namespace ChipControl.Domain.Interfaces;

using ChipControl.Domain.Entities;

public interface IFuncionarioRepository
{
    Task<Funcionario?> BuscarPorIdAsync(int id);
    Task<IEnumerable<Funcionario>> ListarAsync();
    Task<bool> ExisteFuncionarioAsync(string nomeCompleto);
    Task<bool> ExisteFuncionarioAsync(string nomeCompleto, int idExato);
    Task AdicionarAsync(Funcionario funcionario);
    Task AtualizarAsync(Funcionario funcionario);
    Task<IEnumerable<Funcionario>> PesquisarAsync(string termo);
}
