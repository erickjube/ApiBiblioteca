using ApiBiblioteca.Domain.Common;
using ApiBiblioteca.Domain.Entities;

namespace ApiBiblioteca.Application.Interfaces.IRepository;

public interface IEmprestimoRepository
{
    Task<PagedList<Emprestimo>> GetAllAsync(int skip, int take);
    Task<PagedList<Multa>> GetMultasByEmprestimo(int emprestimoId, int skip, int take);
    Task<PagedList<ItemEmprestimo>> GetItensByEmprestimo(int emprestimoId, int skip, int take);
    Task<Emprestimo> GetByIdAsync(int emprestimoId);
    Task<ItemEmprestimo> GetItemByIdAsync(int itemId);
    Task<Emprestimo?> GetMultas(int emprestimoId);
    Task AddAsync(Emprestimo emprestimo);
    Task<bool> ClienteTemEmprestimoAtivo(int clienteId);
}
