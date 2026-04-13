using ApiBiblioteca.Domain.Entities;

namespace ApiBiblioteca.Application.Interfaces.IRepository;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task<Cliente> GetByIdAsync(int id);
    Task<IEnumerable<Cliente>> GetByNameAsync(string nome);
    Task<Cliente> GetClienteComEmprestimosAsync(int id);
    Task<Cliente> GetClienteComVendasAsync(int id);
    void Create(Cliente cliente);
    void Remove(Cliente cliente);
    Task<bool> Existe(string cpf, string email, string telefone);
}
