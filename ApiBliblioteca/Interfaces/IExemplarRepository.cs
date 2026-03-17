using ApiBiblioteca.Domain.Entities;

namespace ApiBiblioteca.Interfaces;

public interface IExemplarRepository
{
    Task<IEnumerable<ExemplarLivro>> GetAllAsync();
    Task<ExemplarLivro> GetByIdAsync(int id);
    Task<IEnumerable<ExemplarLivro>> GetByNameAsync(string nome);
    void Create(ExemplarLivro exemplar);
    void Remove(ExemplarLivro exemplar);
    Task SaveAsync();
    Task<bool> ExisteCodigoBarras(string codigoBarras);
}
