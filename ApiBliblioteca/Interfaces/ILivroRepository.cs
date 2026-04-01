using ApiBiblioteca.Domain.Entities;

namespace ApiBiblioteca.Domain.Interfaces;

public interface ILivroRepository
{
    Task<IEnumerable<Livro>> GetAllAsync();
    Task<Livro> GetByIdAsync(long id);
    Task<Livro> GetLivroComExemplaresAsync(long id);
    Task<IEnumerable<Livro>> GetByNameComExemplaresAsync(string titulo);
    Task<bool> ExistsByIsbn(string isbn);
    void Create(Livro livro);
    void Remove(Livro livro);
}
