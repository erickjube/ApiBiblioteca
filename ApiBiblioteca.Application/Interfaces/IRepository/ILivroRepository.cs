using ApiBiblioteca.Domain.Common;
using ApiBiblioteca.Domain.Entities;

namespace ApiBiblioteca.Application.Interfaces.IRepository;

public interface ILivroRepository
{
    Task<PagedList<Livro>> GetAllAsync(int skip, int take);
    Task<PagedList<ExemplarLivro>> GetExemplaresByLivroAsync(int livroId, int skip, int take);
    Task<Livro> GetByIdAsync(int id);
    Task<bool> ExistsByIsbn(string isbn);
    void Create(Livro livro);
    void Remove(Livro livro);
}
