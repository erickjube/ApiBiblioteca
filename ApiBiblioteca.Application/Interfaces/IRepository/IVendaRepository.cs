using ApiBiblioteca.Domain.Entities;

namespace ApiBiblioteca.Application.Interfaces.IRepository;

public interface IVendaRepository
{
    Task<IEnumerable<Venda>> GetAllAsync();
    Task<Venda?> GetByIdAsync(int vendaId);
    Task AddAsync(Venda venda);
}
