using ApiBiblioteca.Domain.Entities;

namespace ApiBiblioteca.Interfaces;

public interface IEmprestimoRepository
{
    Task<IEnumerable<Emprestimo>> GetAllAsync();
    Task<Emprestimo?> GetByIdAsync(int emprestimoId);
    Task AddAsync(Emprestimo emprestimo);
    Task<bool> ClienteTemEmprestimoAtivo(int clienteId);
    Task SaveChanges();
}
