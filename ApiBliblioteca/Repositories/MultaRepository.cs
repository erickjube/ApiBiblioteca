using ApiBiblioteca.Domain.Context;
using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.Entities;
using ApiBiblioteca.Interfaces;

namespace ApiBiblioteca.Repositories;

public class MultaRepository : IMultaRepository
{
    private readonly MeuDbContext _context;

    public MultaRepository(MeuDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Multa multa)
    {
        await _context.Multa.AddAsync(multa);
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}
