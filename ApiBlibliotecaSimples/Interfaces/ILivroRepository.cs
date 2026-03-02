using ApiBibliotecaSimples.Domain.Entities;

namespace ApiBibliotecaSimples.Domain.Interfaces;

public interface ILivroRepository
{
    Task<IEnumerable<Livro>> GetAllAsync();
    Task<Livro> GetByIdAsync(long id);
    Task<IEnumerable<Livro>> GetByNameAsync(string titulo);
    Task<bool> ExistsByIsbn(string isbn);
    void Create(Livro livro);
    Task SaveAsync();
    void Remove(Livro livro);
}
