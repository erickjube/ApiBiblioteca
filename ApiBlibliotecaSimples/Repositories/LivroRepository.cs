using ApiBibliotecaSimples.Domain.Context;
using ApiBibliotecaSimples.Domain.Entities;
using ApiBibliotecaSimples.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBibliotecaSimples.Domain.Repositories;

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

    public async Task<IEnumerable<Livro>> GetByNameAsync(string titulo)
    {
        return await _context.Livro.Where(x => x.Titulo.Contains(titulo)).ToListAsync();
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
