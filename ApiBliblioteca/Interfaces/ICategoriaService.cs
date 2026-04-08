using ApiBiblioteca.DTOs.DtosCategoria;

namespace ApiBiblioteca.Interfaces;

public interface ICategoriaService
{
    public Task<IEnumerable<CategoriaResponseDto>> Get();
    public Task<CategoriaComLivrosDto> GetComLivros(long id);
    public Task<IEnumerable<CategoriaComLivrosDto>> GetByNameComLivros(string nome);
    public Task<CategoriaResponseDto> GetId(long id);
    public Task<CategoriaResponseDto> Create(CategoriaDto dto);
    public Task<CategoriaResponseDto> Update(long id, CategoriaDto dto);
    public Task Delete(long id);
}
