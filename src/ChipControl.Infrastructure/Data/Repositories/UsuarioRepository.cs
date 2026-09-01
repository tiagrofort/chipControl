using ChipControl.Domain.Entities;
using ChipControl.Domain.Interfaces;
using ChipControl.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ChipControl.Infrastructure.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ChipControlDbContext _context;

    public UsuarioRepository(ChipControlDbContext context)
    {
        _context = context;
    }

    public async Task<UsuarioSistema?> BuscarPorLoginAsync(string login)
    {
        return await _context.UsuariosSistema
            .FirstOrDefaultAsync(u => u.Login == login);
    }

    public async Task<UsuarioSistema?> BuscarPorIdAsync(int id)
    {
        return await _context.UsuariosSistema.FindAsync(id);
    }

    public async Task<bool> ExisteLoginAsync(string login)
    {
        return await _context.UsuariosSistema
            .AnyAsync(u => u.Login == login);
    }

    public async Task<bool> ExisteLoginAsync(string login, int idExato)
    {
        return await _context.UsuariosSistema
            .AnyAsync(u => u.Login == login && u.Id != idExato);
    }

    public async Task AdicionarAsync(UsuarioSistema usuario)
    {
        await _context.UsuariosSistema.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(UsuarioSistema usuario)
    {
        _context.UsuariosSistema.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<UsuarioSistema>> ListarAsync()
    {
        return await _context.UsuariosSistema
            .OrderBy(u => u.Nome)
            .ToListAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.UsuariosSistema.CountAsync();
    }
}
