namespace ChipControl.Infrastructure.Data.Repositories;

using ChipControl.Domain.Entities;
using ChipControl.Domain.Interfaces;
using ChipControl.Persistence;
using Microsoft.EntityFrameworkCore;

public class OperadoraRepository : IOperadoraRepository
{
    private readonly ChipControlDbContext _context;

    public OperadoraRepository(ChipControlDbContext context)
    {
        _context = context;
    }

    public async Task<Operadora?> BuscarPorIdAsync(int id)
    {
        return await _context.Operadoras
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Operadora>> ListarAsync()
    {
        return await _context.Operadoras
            .OrderBy(o => o.Nome)
            .ToListAsync();
    }

    public async Task<bool> ExisteOperadoraAsync(string nome)
    {
        return await _context.Operadoras
            .AnyAsync(o => o.Nome == nome);
    }

    public async Task<bool> ExisteOperadoraAsync(string nome, int idExato)
    {
        return await _context.Operadoras
            .AnyAsync(o => o.Nome == nome && o.Id != idExato);
    }

    public async Task<bool> ExisteCnpjAsync(string cnpj)
    {
        return await _context.Operadoras
            .AnyAsync(o => o.Cnpj == cnpj);
    }

    public async Task<bool> ExisteCnpjAsync(string cnpj, int idExato)
    {
        return await _context.Operadoras
            .AnyAsync(o => o.Cnpj == cnpj && o.Id != idExato);
    }

    public async Task AdicionarAsync(Operadora operadora)
    {
        await _context.Operadoras.AddAsync(operadora);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Operadora operadora)
    {
        _context.Operadoras.Update(operadora);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Operadora>> PesquisarAsync(string termo)
    {
        return await _context.Operadoras
            .Where(o =>
                o.Nome.Contains(termo) ||
                (o.Codigo != null && o.Codigo.Contains(termo)) ||
                (o.Cnpj != null && o.Cnpj.Contains(termo)) ||
                (o.Telefone != null && o.Telefone.Contains(termo)) ||
                (o.Email != null && o.Email.Contains(termo)) ||
                (o.Observacoes != null && o.Observacoes.Contains(termo)) ||
                o.Id.ToString().Contains(termo))
            .OrderBy(o => o.Nome)
            .ToListAsync();
    }

    public async Task<IEnumerable<Operadora>> ListarAtivasAsync()
    {
        return await _context.Operadoras
            .Where(o => o.Ativo)
            .OrderBy(o => o.Nome)
            .ToListAsync();
    }
}