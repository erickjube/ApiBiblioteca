using ApiBiblioteca.Application.DTOs.DtosCategoria;
using ApiBiblioteca.Application.DTOs.DtosLivro;
using ApiBiblioteca.Application.Pagination;
using ApiBiblioteca.Domain.Common;

namespace ApiBiblioteca.Application.Interfaces.IServices;

public interface ICategoriaService
{
    public Task<PagedList<CategoriaResponseDto>> Get(QueryParameters parameters);
    public Task<PagedList<LivroResponseDto>> GetComLivros(long categoriaId, QueryParameters parameters);
    public Task<CategoriaResponseDto> GetId(long id);
    public Task<CategoriaResponseDto> Create(CategoriaDto dto);
    public Task<CategoriaResponseDto> Update(long id, CategoriaDto dto);
    public Task Delete(long id);
}
