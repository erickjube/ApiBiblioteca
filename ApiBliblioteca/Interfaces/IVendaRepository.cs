using ApiBiblioteca.Domain.Entities;

namespace ApiBiblioteca.Interfaces;

public interface IVendaRepository
{
    Task<IEnumerable<Venda>> GetAllAsync();
    Task<Venda?> GetByIdAsync(int vendaId);
    Task AddAsync(Venda venda);
    Task SaveAsync();
}
