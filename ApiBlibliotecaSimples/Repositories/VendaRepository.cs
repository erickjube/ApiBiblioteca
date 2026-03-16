using ApiBibliotecaSimples.Domain.Context;
using ApiBibliotecaSimples.Domain.Entities;
using ApiBlibliotecaSimples.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBlibliotecaSimples.Repositories;

public class VendaRepository : IVendaRepository
{
    private readonly MeuDbContext _context;

    public VendaRepository(MeuDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Venda>> GetAllAsync()
    {
        return await _context.Venda.ToListAsync();
    }

    public async Task<Venda?> GetByIdAsync(int VendaId)
    {
        return await _context.Venda.Include(e => e.Itens).ThenInclude(i => i.Exemplar).FirstOrDefaultAsync(e => e.Id == VendaId);
    }

    public async Task AddAsync(Venda Venda)
    {
        await _context.Venda.AddAsync(Venda);
    }

    public async Task SaveAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro");
        }
    }
}
