using ApiBiblioteca.Domain.Entities;

namespace ApiBiblioteca.Interfaces;

public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> GetAllAsync();
    Task<Categoria> GetCategoriaComLivrosAsync(long id);
    Task<IEnumerable<Categoria>> GetByNameComLivrosAsync(string nome);
    Task<Categoria> GetByIdAsync(long id);
    void Create(Categoria categoria);
    void Remove(Categoria categoria);
}
