using ApiBiblioteca.Domain.Context;
using ApiBiblioteca.Interfaces;

namespace ApiBiblioteca.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MeuDbContext _context;
    public UnitOfWork(MeuDbContext context)
    {
        _context = context;
    }
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
