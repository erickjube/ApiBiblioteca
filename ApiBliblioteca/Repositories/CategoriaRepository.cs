using ApiBiblioteca.Domain.Context;
using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    protected readonly MeuDbContext _context;

    public CategoriaRepository(MeuDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        return await _context.Categoria.ToListAsync();
    }

   public async Task<Categoria> GetCategoriaComLivrosAsync(long id)
    {
        return await _context.Categoria.Include(c => c.Livros).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Categoria>> GetByNameComLivrosAsync(string nome)
    {
        return await _context.Categoria.Include(c => c.Livros).Where(c => c.Nome.Contains(nome)).ToListAsync();
    }

    public async Task<Categoria?> GetByIdAsync(long id)
    {
        return await _context.Categoria.FirstOrDefaultAsync(c => c.Id == id);
    }

    public void Create(Categoria categoria)
    {
        _context.Categoria.AddAsync(categoria);
    }

    public void Remove(Categoria categoria)
    {
        _context.Categoria.Remove(categoria);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
