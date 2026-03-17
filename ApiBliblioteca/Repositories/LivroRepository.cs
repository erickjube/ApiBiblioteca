using ApiBiblioteca.Domain.Context;
using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Domain.Repositories;

public class LivroRepository : ILivroRepository
{
    protected readonly MeuDbContext _context;

    public LivroRepository(MeuDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Livro>> GetAllAsync()
    {
        return await _context.Livro.ToListAsync();       
    }

    public async Task<Livro?> GetByIdAsync(long id)
    {
        return await _context.Livro.FirstOrDefaultAsync(x => x.Id == id); 
    }

    public async Task<Livro?> GetLivroComExemplaresAsync(long id)
    {
        return await _context.Livro.Include(l => l.Exemplares).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Livro>> GetByNameComExemplaresAsync(string titulo)
    {
        return await _context.Livro.Include(l => l.Exemplares).Where(x => x.Titulo.Contains(titulo)).ToListAsync();
    }

    public async Task<bool> ExistsByIsbn(string isbn)
    {
        return await _context.Livro.AnyAsync(x => x.Isbn == isbn);
    }

    public void Create(Livro livro)
    {
        _context.Livro.AddAsync(livro);
    }

    public void Remove(Livro livro)
    {
        _context.Livro.Remove(livro);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
