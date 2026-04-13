using ApiBiblioteca.ApiBiblioteca.Infrastructure.Data;
using ApiBiblioteca.Application.Interfaces.IRepository;
using ApiBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Repositories;

public class MultaRepository : IMultaRepository
{
    private readonly MeuDbContext _context;

    public MultaRepository(MeuDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Multa>> GetAllAsync()
    {
        return await _context.Multa.ToListAsync();
    }

    public async Task<Multa?> GetByIdAsync(int multaId)
    {
        return await _context.Multa.FirstOrDefaultAsync(m => m.Id == multaId);
    }

    public async Task AddAsync(Multa multa)
    {
        await _context.Multa.AddAsync(multa);
    }
}
