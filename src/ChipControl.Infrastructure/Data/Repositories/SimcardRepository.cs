namespace ChipControl.Infrastructure.Data.Repositories;

using ChipControl.Domain.Entities;
using ChipControl.Domain.Interfaces;
using ChipControl.Persistence;
using Microsoft.EntityFrameworkCore;

public class SimcardRepository : ISimcardRepository
{
    private readonly ChipControlDbContext _context;

    public SimcardRepository(ChipControlDbContext context)
    {
        _context = context;
    }

    public async Task<Simcard?> BuscarPorIdAsync(int id)
    {
        return await _context.Simcards
            .Include(s => s.Operadora)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Simcard>> ListarAsync()
    {
        return await _context.Simcards
            .Include(s => s.Operadora)
            .OrderBy(s => s.Iccid)
            .ToListAsync();
    }

    public async Task<IEnumerable<Simcard>> PesquisarAsync(string termo)
    {
        return await _context.Simcards
            .Include(s => s.Operadora)
            .Where(s =>
                s.Iccid.Contains(termo) ||
                s.IdentificacaoChip.Contains(termo) ||
                s.Operadora.Nome.Contains(termo) ||
                (s.Ddd != null && s.Ddd.Contains(termo)) ||
                (s.PlanoTipo != null && s.PlanoTipo.Contains(termo)) ||
                (s.Observacoes != null && s.Observacoes.Contains(termo)) ||
                s.Id.ToString().Contains(termo))
            .OrderBy(s => s.Iccid)
            .ToListAsync();
    }

    public async Task<bool> ExisteIccidAsync(string iccid)
    {
        return await _context.Simcards.AnyAsync(s => s.Iccid == iccid);
    }

    public async Task<bool> ExisteIccidAsync(string iccid, int idExato)
    {
        return await _context.Simcards.AnyAsync(s => s.Iccid == iccid && s.Id != idExato);
    }

    public async Task<bool> ExisteIdentificacaoNaOperadoraAsync(string identificacaoChip, int operadoraId)
    {
        return await _context.Simcards
            .AnyAsync(s => s.IdentificacaoChip == identificacaoChip && s.OperadoraId == operadoraId);
    }

    public async Task<bool> ExisteIdentificacaoNaOperadoraAsync(string identificacaoChip, int operadoraId, int idExato)
    {
        return await _context.Simcards
            .AnyAsync(s => s.IdentificacaoChip == identificacaoChip && s.OperadoraId == operadoraId && s.Id != idExato);
    }

    public async Task AdicionarAsync(Simcard simcard)
    {
        await _context.Simcards.AddAsync(simcard);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Simcard simcard)
    {
        _context.Simcards.Update(simcard);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Operadora>> ListarOperadorasAsync()
    {
        return await _context.Operadoras
            .Where(o => o.Ativo)
            .OrderBy(o => o.Nome)
            .ToListAsync();
    }
}