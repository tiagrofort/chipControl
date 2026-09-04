namespace ChipControl.Domain.Interfaces;

using ChipControl.Domain.Entities;

public interface IOperadoraRepository
{
    Task<Operadora?> BuscarPorIdAsync(int id);
    Task<IEnumerable<Operadora>> ListarAsync();
    Task<bool> ExisteOperadoraAsync(string nome);
    Task<bool> ExisteOperadoraAsync(string nome, int idExato);
    Task<bool> ExisteCnpjAsync(string cnpj);
    Task<bool> ExisteCnpjAsync(string cnpj, int idExato);
    Task AdicionarAsync(Operadora operadora);
    Task AtualizarAsync(Operadora operadora);
    Task<IEnumerable<Operadora>> PesquisarAsync(string termo);
    Task<IEnumerable<Operadora>> ListarAtivasAsync();
}