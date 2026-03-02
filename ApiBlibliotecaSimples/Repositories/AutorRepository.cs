using ApiBibliotecaSimples.Domain.Context;
using ApiBibliotecaSimples.Domain.Entities;
using ApiBlibliotecaSimples.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBlibliotecaSimples.Repositories;

public class AutorRepository : IAutorRepository
{
    protected readonly MeuDbContext _context;

    public AutorRepository(MeuDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Autor>> GetAllAsync()
    {
        return await _context.Autor.ToListAsync();
    }

    public async Task<Autor?> GetAutorComLivrosAsync(long id)
    {
        return await _context.Autor.Include(a => a.Livros).FirstOrDefaultAsync(a => a.Id == id);
    }

    public Task<Autor?> GetByIdAsync(long id)
    {
        return _context.Autor.FirstOrDefaultAsync(a => a.Id == id);
    }

    public void Create(Autor autor)
    {
        _context.Autor.AddAsync(autor);
    }

    public void Remove(Autor autor)
    {
        _context.Autor.Remove(autor);
    }

    public Task SaveAsync()
    {
        return _context.SaveChangesAsync();
    }
}
