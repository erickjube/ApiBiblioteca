using ApiBiblioteca.Domain.Entities;

namespace ApiBiblioteca.Application.Interfaces.IRepository;

public interface IAutorRepository
{
    Task<IEnumerable<Autor>> GetAllAsync();
    Task<Autor> GetAutorComLivrosAsync(long id);
    Task<Autor> GetByIdAsync(long id);
    Task<IEnumerable<Autor>> GetByNameComLivrosAsync(string nome);
    void Create(Autor autor);
    void Remove(Autor autor);
}
