using ApiBiblioteca.ApiBiblioteca.Infrastructure.Data;
using ApiBiblioteca.Application.Interfaces.IRepository;
using ApiBiblioteca.Domain.Common;
using ApiBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    protected readonly MeuDbContext _context;

    public CategoriaRepository(MeuDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<Categoria>> GetAllAsync(int skip, int take)
    {
        var totalCont = await _context.Categoria.CountAsync();
        var data = await _context.Categoria.Skip(skip).Take(take).ToListAsync();
        return new PagedList<Categoria> { Data = data, TotalCount = totalCont };
    }

   public async Task<PagedList<Livro>> GetLivrosByCategoriaAsync(long categoriaId, int skip, int take)
    {
        var query = _context.Livro.Where(l => l.CategoriaId == categoriaId);
        var totalCount = await query.CountAsync();
        var data = await query.Skip(skip).Take(take).ToListAsync();
        return new PagedList<Livro> { Data = data, TotalCount = totalCount };
    }

    public async Task<Categoria?> GetByIdAsync(long categoriaId)
    {
        return await _context.Categoria.FirstOrDefaultAsync(c => c.Id == categoriaId);
    }

    public void Create(Categoria categoria)
    {
        _context.Categoria.AddAsync(categoria);
    }

    public void Remove(Categoria categoria)
    {
        _context.Categoria.Remove(categoria);
    }
}
