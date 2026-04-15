using ApiBiblioteca.Domain.Common;
using ApiBiblioteca.Domain.Entities;

namespace ApiBiblioteca.Application.Interfaces.IRepository;

public interface IExemplarRepository
{
    Task<PagedList<ExemplarLivro>> GetAllAsync(int skip, int take);
    Task<ExemplarLivro> GetByIdAsync(int id);
    void Create(ExemplarLivro exemplar);
    void Remove(ExemplarLivro exemplar);
    Task<bool> ExisteCodigoBarras(string codigoBarras);
}
