using ApiBiblioteca.ApiBiblioteca.Infrastructure.Context;
using ApiBiblioteca.Application.Interfaces;

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
