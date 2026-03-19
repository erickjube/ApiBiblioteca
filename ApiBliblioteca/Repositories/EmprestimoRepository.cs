using ApiBiblioteca.Domain.Context;
using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.ENUMs;
using ApiBiblioteca.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Repositories;

public class EmprestimoRepository : IEmprestimoRepository
{
    private readonly MeuDbContext _context;

    public EmprestimoRepository(MeuDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Emprestimo>> GetAllAsync()
    {
        return await _context.Emprestimo.ToListAsync();
    }

    public async Task<Emprestimo?> GetByIdAsync(int emprestimoId)
    {
        return await _context.Emprestimo.Include(e => e.Itens).ThenInclude(i => i.Exemplar).FirstOrDefaultAsync(e => e.Id == emprestimoId);
    }

    public async Task<Emprestimo?> GetEmprestimoComMultas(int emprestimoId)
    {
        return await _context.Emprestimo.Include(i => i.Multas).FirstOrDefaultAsync(e => e.Id == emprestimoId);
    }

    public async Task AddAsync(Emprestimo emprestimo)
    {
        await _context.Emprestimo.AddAsync(emprestimo);
    }

    public async Task<bool> ClienteTemEmprestimoAtivo(int clienteId)
    {
        return await _context.Emprestimo.AnyAsync(e => e.ClienteId == clienteId && e.Status == StatusEmprestimo.Ativo);
    }


    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}
