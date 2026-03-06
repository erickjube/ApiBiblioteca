using ApiBlibliotecaSimples.DTOs;

namespace ApiBlibliotecaSimples.Interfaces;

public interface IAutorService
{
    public Task<IEnumerable<DtoResponseAutor>> Get();
    public Task<DtoAutorComLivros> GetComLivros(long id);
    public Task<DtoResponseAutor> GetId(long id);
    public Task<IEnumerable<DtoAutorComLivros>> GetByNameComLivros(string nome);
    public Task<DtoResponseAutor> Create(DtoAutor dto);
    public Task<DtoResponseAutor> Update(long id, DtoAutor dto);
    public Task Delete(long id);
}
