using ApiBiblioteca.Domain.Entities;

namespace ApiBiblioteca.Application.Interfaces.IRepository;

public interface IEmprestimoRepository
{
    Task<IEnumerable<Emprestimo>> GetAllAsync();
    Task<Emprestimo?> GetByIdAsync(int emprestimoId);
    Task<Emprestimo?> GetEmprestimoComMultas(int emprestimoId);
    Task AddAsync(Emprestimo emprestimo);
    Task<bool> ClienteTemEmprestimoAtivo(int clienteId);
}
