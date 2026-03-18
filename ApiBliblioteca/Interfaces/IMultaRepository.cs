using ApiBiblioteca.Entities;

namespace ApiBiblioteca.Interfaces;

public interface IMultaRepository
{
    Task AddAsync(Multa multa);
    Task SaveChanges();
}
