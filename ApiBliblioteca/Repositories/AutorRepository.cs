using ApiBiblioteca.Domain.Context;
using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Repositories;

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

    public async Task<Autor?> GetByIdAsync(long id)
    {
        return await _context.Autor.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Autor?>> GetByNameComLivrosAsync(string nome)
    {
        return await _context.Autor.Include(a => a.Livros).Where(c => c.Nome.Contains(nome)).ToListAsync();
    }

    public void Create(Autor autor)
    {
        _context.Autor.AddAsync(autor);
    }

    public void Remove(Autor autor)
    {
        _context.Autor.Remove(autor);
    }
}
