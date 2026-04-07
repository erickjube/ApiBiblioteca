using ApiBiblioteca.DTOs.DtosCategoria;

namespace ApiBiblioteca.Interfaces;

public interface ICategoriaService
{
    public Task<IEnumerable<DtoResponseCategoria>> Get();
    public Task<DtoCategoriaComLivros> GetComLivros(long id);
    public Task<IEnumerable<DtoCategoriaComLivros>> GetByNameComLivros(string nome);
    public Task<DtoResponseCategoria> GetId(long id);
    public Task<DtoResponseCategoria> Create(DtoCategoria dto);
    public Task<DtoResponseCategoria> Update(long id, DtoCategoria dto);
    public Task Delete(long id);
}
