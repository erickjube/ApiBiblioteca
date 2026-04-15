using ApiBiblioteca.ApiBiblioteca.Infrastructure.Data;
using ApiBiblioteca.Application.Interfaces.IRepository;
using ApiBiblioteca.Domain.Common;
using ApiBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Repositories;

public class AutorRepository : IAutorRepository
{
    protected readonly MeuDbContext _context;

    public AutorRepository(MeuDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<Autor>> GetAllAsync(int skip, int take)
    {
        var totalCont = await _context.Autor.CountAsync();
        var data = await _context.Autor.Skip(skip).Take(take).ToListAsync();
        return new PagedList<Autor> { Data = data, TotalCount = totalCont };
    }

    public async Task<PagedList<Livro>> GetLivrosByAutorIdAsync(long autorId, int skip, int take)
    {
        var query = _context.Livro.Where(l => l.AutorId == autorId);
        var totalCount = await query.CountAsync();
        var data = await query.Skip(skip).Take(take).ToListAsync();
        return new PagedList<Livro> { Data = data, TotalCount = totalCount };
    }

    public async Task<Autor?> GetByIdAsync(long autorId)
    {
        return await _context.Autor.FirstOrDefaultAsync(a => a.Id == autorId);
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
