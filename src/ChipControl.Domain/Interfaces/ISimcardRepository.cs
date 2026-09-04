namespace ChipControl.Domain.Interfaces;

using ChipControl.Domain.Entities;

public interface ISimcardRepository
{
    Task<Simcard?> BuscarPorIdAsync(int id);
    Task<IEnumerable<Simcard>> ListarAsync();
    Task<IEnumerable<Simcard>> PesquisarAsync(string termo);
    Task<bool> ExisteIccidAsync(string iccid);
    Task<bool> ExisteIccidAsync(string iccid, int idExato);
    Task<bool> ExisteIdentificacaoNaOperadoraAsync(string identificacaoChip, int operadoraId);
    Task<bool> ExisteIdentificacaoNaOperadoraAsync(string identificacaoChip, int operadoraId, int idExato);
    Task AdicionarAsync(Simcard simcard);
    Task AtualizarAsync(Simcard simcard);
    Task<IEnumerable<Operadora>> ListarOperadorasAsync();
}
