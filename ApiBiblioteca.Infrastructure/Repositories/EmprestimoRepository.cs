using ApiBiblioteca.ApiBiblioteca.Infrastructure.Data;
using ApiBiblioteca.Application.Interfaces.IRepository;
using ApiBiblioteca.Domain.Common;
using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.Domain.ENUMs;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Repositories;

public class EmprestimoRepository : IEmprestimoRepository
{
    private readonly MeuDbContext _context;

    public EmprestimoRepository(MeuDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<Emprestimo>> GetAllAsync(int skip, int take)
    {
        var totalCont = await _context.Emprestimo.CountAsync();
        var data = await _context.Emprestimo.Skip(skip).Take(take).ToListAsync();
        return new PagedList<Emprestimo> { Data = data, TotalCount = totalCont };
    }

    public async Task<PagedList<Multa>> GetMultasByEmprestimo(int emprestimoId, int skip, int take)
    {
        var query = _context.Multa.Where(l => l.EmprestimoId == emprestimoId);
        var totalCount = await query.CountAsync();
        var data = await query.Skip(skip).Take(take).ToListAsync();
        return new PagedList<Multa> { Data = data, TotalCount = totalCount };
    }

    public async Task<PagedList<ItemEmprestimo>> GetItensByEmprestimo(int emprestimoId, int skip, int take)
    {
        var query = _context.ItemEmprestimo.Where(i => i.EmprestimoId == emprestimoId).Include(i => i.Exemplar);
        var totalCount = await query.CountAsync();
        var data = await query.Skip(skip).Take(take).ToListAsync();
        return new PagedList<ItemEmprestimo> { Data = data, TotalCount = totalCount };
    }

    public async Task<Emprestimo?> GetByIdAsync(int emprestimoId)
    {
        return await _context.Emprestimo.Include(e => e.Itens).FirstOrDefaultAsync(e => e.Id == emprestimoId);
    }

    public async Task<ItemEmprestimo> GetItemByIdAsync(int itemId)
    {
        return await _context.ItemEmprestimo.Include(i => i.Exemplar).FirstOrDefaultAsync(i => i.Id == itemId);
    }

    public async Task<Emprestimo?> GetMultas(int emprestimoId)
    {
        return await _context.Emprestimo.Include(e => e.Multas).Include(e => e.Itens).FirstOrDefaultAsync(e => e.Id == emprestimoId);
    }

    public async Task AddAsync(Emprestimo emprestimo)
    {
        await _context.Emprestimo.AddAsync(emprestimo);
    }

    public async Task<bool> ClienteTemEmprestimoAtivo(int clienteId)
    {
        return await _context.Emprestimo.AnyAsync(e => e.ClienteId == clienteId && e.Status == StatusEmprestimo.Ativo);
    }
}
