using ApiBibliotecaSimples.Domain.Entities;

namespace ApiBlibliotecaSimples.Interfaces;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task<Cliente> GetByIdAsync(int id);
    Task<IEnumerable<Cliente>> GetByNameAsync(string nome);
    Task<Cliente> GetClienteComEmprestimosAsync(int id);
    Task<Cliente> GetClienteComVendasAsync(int id);
    void Create(Cliente cliente);
    void Remove(Cliente cliente);
    Task SaveAsync();
    Task<bool> ExisteCpf(string cpf);   
}
