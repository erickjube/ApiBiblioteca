using ApiBiblioteca.Domain.Entities;

namespace ApiBiblioteca.Application.Interfaces.IRepository;

public interface IMultaRepository
{
    Task<IEnumerable<Multa>> GetAllAsync();
    Task<Multa?> GetByIdAsync(int multaId);
    Task AddAsync(Multa multa);
}
