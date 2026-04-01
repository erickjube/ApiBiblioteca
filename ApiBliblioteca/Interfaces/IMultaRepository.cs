using ApiBiblioteca.Entities;

namespace ApiBiblioteca.Interfaces;

public interface IMultaRepository
{
    Task<IEnumerable<Multa>> GetAllAsync();
    Task<Multa?> GetByIdAsync(int multaId);
    Task AddAsync(Multa multa);
}
