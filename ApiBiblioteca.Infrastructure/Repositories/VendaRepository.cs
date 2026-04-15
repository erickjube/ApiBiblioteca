using ApiBiblioteca.ApiBiblioteca.Infrastructure.Data;
using ApiBiblioteca.Application.Interfaces.IRepository;
using ApiBiblioteca.Domain.Common;
using ApiBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Repositories;

public class VendaRepository : IVendaRepository
{
    private readonly MeuDbContext _context;

    public VendaRepository(MeuDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<Venda>> GetAllAsync(int skip, int take)
    {
        var totalCount = await _context.Venda.CountAsync();
        var data = await _context.Venda.Skip(skip).Take(take).ToListAsync();
        return new PagedList<Venda> { Data = data, TotalCount = totalCount };
    }

    public async Task<Venda?> GetByIdAsync(int id)
    {
        return await _context.Venda.FindAsync(id);
    }

    public async Task<PagedList<ItemVenda>> GetItensVendaByIdAsync(int vendaId, int skip, int take)
    {
        var query = _context.ItemVenda.Where(i => i.VendaId == vendaId).Include(i => i.Exemplar);
        var totalCount = await query.CountAsync();
        var data = await query.Skip(skip).Take(take).ToListAsync();
        return new PagedList<ItemVenda> { Data = data, TotalCount = totalCount };
    }

    public async Task AddAsync(Venda Venda)
    {
        await _context.Venda.AddAsync(Venda);
    }
}
