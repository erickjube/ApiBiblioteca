using ApiBiblioteca.ApiBiblioteca.Infrastructure.Data;
using ApiBiblioteca.Application.Interfaces.IRepository;
using ApiBiblioteca.Domain.Common;
using ApiBiblioteca.Domain.Entities;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Repositories;

public class ExemplarRepository : IExemplarRepository
{
    private readonly MeuDbContext _context;
    public ExemplarRepository(MeuDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<ExemplarLivro>> GetAllAsync(int skip, int take)
    {
        var totalCont = await _context.Exemplar.CountAsync();
        var data = await _context.Exemplar.Skip(skip).Take(take).ToListAsync();
        return new PagedList<ExemplarLivro> { Data = data, TotalCount = totalCont };
    }

    public async Task<ExemplarLivro?> GetByIdAsync(int id)
    {
        return await _context.Exemplar.FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Create(ExemplarLivro exemplar)
    {
        _context.Exemplar.AddAsync(exemplar);
    }

    public void Remove(ExemplarLivro exemplar)
    {
        _context.Exemplar.Remove(exemplar);
    }

    public async Task<bool> ExisteCodigoBarras(string codigoBarras)
    {
        return await _context.Exemplar.AnyAsync(x => x.CodigoDeBarras == codigoBarras);
    }

}
