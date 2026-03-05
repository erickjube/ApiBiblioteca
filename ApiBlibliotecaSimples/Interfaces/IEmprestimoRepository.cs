using ApiBibliotecaSimples.Domain.Entities;

namespace ApiBlibliotecaSimples.Interfaces;

public interface IEmprestimoRepository
{
    public Task<IEnumerable<Emprestimo>> GetAllAsync();
}
