using ApiBiblioteca.Domain.Context;
using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Repositories;

public class ExemplarRepository : IExemplarRepository
{
    private readonly MeuDbContext _context;
    public ExemplarRepository(MeuDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExemplarLivro>> GetAllAsync()
    {
        return await _context.Exemplar.ToListAsync();
    }

    public async Task<ExemplarLivro?> GetByIdAsync(int id)
    {
        return await _context.Exemplar.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<ExemplarLivro?>> GetByNameAsync(string nome)
    {
        return await _context.Exemplar.Where(x => x.Nome.Contains(nome)).ToListAsync();
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
