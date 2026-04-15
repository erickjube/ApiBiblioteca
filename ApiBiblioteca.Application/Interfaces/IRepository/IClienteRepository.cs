using ApiBiblioteca.Domain.Common;
using ApiBiblioteca.Domain.Entities;

namespace ApiBiblioteca.Application.Interfaces.IRepository;

public interface IClienteRepository
{
    Task<PagedList<Cliente>> GetAllAsync(int skip, int take);
    Task<PagedList<Emprestimo>> GetEmprestimosByClienteAsync(int clienteId, int skip, int take);
    Task<PagedList<Venda>> GetVendasByClienteAsync(int clienteId, int skip, int take);
    Task<Cliente> GetByIdAsync(int clienteId);
    void Create(Cliente cliente);
    void Remove(Cliente cliente);
    Task<bool> Existe(string cpf, string email, string telefone);
}
