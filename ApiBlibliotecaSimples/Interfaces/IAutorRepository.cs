using ApiBibliotecaSimples.Domain.Entities;

namespace ApiBlibliotecaSimples.Interfaces;

public interface IAutorRepository
{
    Task<IEnumerable<Autor>> GetAllAsync();
    Task<Autor> GetAutorComLivrosAsync(long id);
    Task<Autor> GetByIdAsync(long id);
    Task<IEnumerable<Autor>> GetByNameComLivrosAsync(string nome);
    void Create(Autor autor);
    Task SaveAsync();
    void Remove(Autor autor);
}
