using ApiBiblioteca.Domain.Common;
using ApiBiblioteca.Domain.Entities;

namespace ApiBiblioteca.Application.Interfaces.IRepository;

public interface ICategoriaRepository
{
    Task<PagedList<Categoria>> GetAllAsync(int skip, int take);
    Task<PagedList<Livro>> GetLivrosByCategoriaAsync(long categoriaId, int skip, int take);
    Task<Categoria> GetByIdAsync(long categoriaId);
    void Create(Categoria categoria);
    void Remove(Categoria categoria);
}
