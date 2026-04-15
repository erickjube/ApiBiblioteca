using ApiBiblioteca.ApiBiblioteca.Infrastructure.Data;
using ApiBiblioteca.Application.Interfaces.IRepository;
using ApiBiblioteca.Domain.Common;
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

    public async Task<PagedList<Cliente>> GetAllAsync(int skip, int take)
    {
        var totalCont = await _context.Cliente.CountAsync();
        var data = await _context.Cliente.Skip(skip).Take(take).ToListAsync();
        return new PagedList<Cliente> { Data = data, TotalCount = totalCont };
    }

    public async Task<PagedList<Emprestimo>> GetEmprestimosByClienteAsync(int clienteId, int skip, int take)
    {
        var query = _context.Emprestimo.Where(l => l.ClienteId == clienteId);
        var totalCount = await query.CountAsync();
        var data = await query.Skip(skip).Take(take).ToListAsync();
        return new PagedList<Emprestimo> { Data = data, TotalCount = totalCount };
    }

    public async Task<PagedList<Venda>> GetVendasByClienteAsync(int clienteId, int skip, int take)
    {
        var query = _context.Venda.Where(l => l.ClienteId == clienteId);
        var totalCount = await query.CountAsync();
        var data = await query.Skip(skip).Take(take).ToListAsync();
        return new PagedList<Venda> { Data = data, TotalCount = totalCount };
    }

    public async Task<Cliente?> GetByIdAsync(int clienteId)
    {
        return await _context.Cliente.FirstOrDefaultAsync(c => c.Id == clienteId);
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