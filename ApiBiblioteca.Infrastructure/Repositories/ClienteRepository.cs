using ApiBiblioteca.ApiBiblioteca.Infrastructure.Data;
using ApiBiblioteca.Application.Interfaces.IRepository;
using ApiBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly MeuDbContext _context;

    public ClienteRepository(MeuDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _context.Cliente.ToListAsync();
    }

    public async Task<Cliente?> GetByIdAsync(int id)
    {
        return await _context.Cliente.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Cliente?>> GetByNameAsync(string nome)
    {
        return await _context.Cliente.Where(c => c.Nome.Contains(nome)).ToListAsync();
    }

    public async Task<Cliente?> GetClienteComEmprestimosAsync(int id)
    {
        return await _context.Cliente.Include(c => c.Emprestimos).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Cliente?> GetClienteComVendasAsync(int id)
    {
        return await _context.Cliente.Include(c => c.Vendas).FirstOrDefaultAsync(c => c.Id == id);
    }

    public void Create(Cliente cliente)
    {
        _context.Cliente.Add(cliente);
    }

    public void Remove(Cliente cliente)
    {
        _context.Cliente.Remove(cliente);
    }

    public async Task<bool> Existe(string cpf, string email, string telefone)
    {
        if ((await _context.Cliente.AnyAsync(x => x.Cpf == cpf)) ||
            (await _context.Cliente.AnyAsync(x => x.Email == email)) ||
            (await _context.Cliente.AnyAsync(x => x.Telefone == telefone)))
            return true;

        return false;
    }
}