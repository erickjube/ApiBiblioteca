using ApiBiblioteca.ApiBiblioteca.Infrastructure.Data;
using ApiBiblioteca.Application.Interfaces.IRepository;
using ApiBiblioteca.Domain.Common;
using ApiBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Domain.Repositories;

public class LivroRepository : ILivroRepository
{
    protected readonly MeuDbContext _context;

    public LivroRepository(MeuDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<Livro>> GetAllAsync(int skip, int take)
    {
        var totalCont = await _context.Livro.CountAsync();
        var data = await _context.Livro.Skip(skip).Take(take).ToListAsync();
        return new PagedList<Livro> { Data = data, TotalCount = totalCont };
    }

    public async Task<PagedList<ExemplarLivro>> GetExemplaresByLivroAsync(long livroId, int skip, int take)
    {
        var query = _context.Exemplar.Where(l => l.LivroId == livroId);
        var totalCount = await query.CountAsync();
        var data = await query.Skip(skip).Take(take).ToListAsync();
        return new PagedList<ExemplarLivro> { Data = data, TotalCount = totalCount };
    }

    public async Task<Livro?> GetByIdAsync(long livroId)
    {
        return await _context.Livro.FirstOrDefaultAsync(x => x.Id == livroId); 
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
}
