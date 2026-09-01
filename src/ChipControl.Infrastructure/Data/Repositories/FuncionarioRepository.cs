namespace ChipControl.Infrastructure.Data.Repositories;

using ChipControl.Domain.Entities;
using ChipControl.Domain.Interfaces;
using ChipControl.Persistence;
using Microsoft.EntityFrameworkCore;

public class FuncionarioRepository : IFuncionarioRepository
{
    private readonly ChipControlDbContext _context;

    public FuncionarioRepository(ChipControlDbContext context)
    {
        _context = context;
    }

    public async Task<Funcionario?> BuscarPorIdAsync(int id)
    {
        return await _context.Funcionarios
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<IEnumerable<Funcionario>> ListarAsync()
    {
        return await _context.Funcionarios
            .OrderBy(f => f.NomeCompleto)
            .ToListAsync();
    }

    public async Task<bool> ExisteFuncionarioAsync(string nomeCompleto)
    {
        return await _context.Funcionarios
            .AnyAsync(f => f.NomeCompleto == nomeCompleto);
    }

    public async Task<bool> ExisteFuncionarioAsync(string nomeCompleto, int idExato)
    {
        return await _context.Funcionarios
            .AnyAsync(f => f.NomeCompleto == nomeCompleto && f.Id != idExato);
    }

    public async Task AdicionarAsync(Funcionario funcionario)
    {
        await _context.Funcionarios.AddAsync(funcionario);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Funcionario funcionario)
    {
        _context.Funcionarios.Update(funcionario);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Funcionario>> PesquisarAsync(string termo)
    {
        return await _context.Funcionarios
            .Where(f =>
                f.NomeCompleto.Contains(termo) ||
                f.Matricula != null && f.Matricula.Contains(termo) ||
                f.Setor.Contains(termo) ||
                (f.Cargo != null && f.Cargo.Contains(termo)) ||
                (f.TelefonePessoal != null && f.TelefonePessoal.Contains(termo)) ||
                (f.Email != null && f.Email.Contains(termo)) ||
                (f.Observacoes != null && f.Observacoes.Contains(termo)) ||
                f.Id.ToString().Contains(termo))
            .OrderBy(f => f.NomeCompleto)
            .ToListAsync();
    }
}
