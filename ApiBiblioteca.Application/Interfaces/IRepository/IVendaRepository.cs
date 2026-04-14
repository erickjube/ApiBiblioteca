using ApiBiblioteca.Domain.Common;
using ApiBiblioteca.Domain.Entities;

namespace ApiBiblioteca.Application.Interfaces.IRepository;

public interface IVendaRepository
{
    Task<PagedList<Venda>> GetAllAsync(int skip, int take);
    Task<Venda?> GetByIdAsync(int id);
    Task<PagedList<ItemVenda>> GetItensVendaByIdAsync(int vendaId, int skip, int take);
    Task AddAsync(Venda venda);
}
