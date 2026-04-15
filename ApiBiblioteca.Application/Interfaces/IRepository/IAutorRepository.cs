using ApiBiblioteca.Domain.Common;
using ApiBiblioteca.Domain.Entities;

namespace ApiBiblioteca.Application.Interfaces.IRepository;

public interface IAutorRepository
{
    Task<PagedList<Autor>> GetAllAsync(int skip, int take);
    Task<PagedList<Livro>> GetLivrosByAutorIdAsync(long autorId, int skip, int take);
    Task<Autor> GetByIdAsync(long autorId);
    void Create(Autor autor);
    void Remove(Autor autor);
}
