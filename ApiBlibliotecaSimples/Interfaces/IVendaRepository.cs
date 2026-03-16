using ApiBibliotecaSimples.Domain.Entities;

namespace ApiBlibliotecaSimples.Interfaces;

public interface IVendaRepository
{
    Task<IEnumerable<Venda>> GetAllAsync();
    Task<Venda?> GetByIdAsync(int vendaId);
    Task AddAsync(Venda venda);
    Task SaveAsync();
}
