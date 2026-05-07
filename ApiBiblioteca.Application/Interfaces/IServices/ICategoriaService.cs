using ApiBiblioteca.Application.DTOs.DtosCategoria;
using ApiBiblioteca.Application.DTOs.DtosLivro;
using ApiBiblioteca.Application.Pagination;
using ApiBiblioteca.Domain.Common;

namespace ApiBiblioteca.Application.Interfaces.IServices;

public interface ICategoriaService
{
    public Task<PagedList<CategoriaResponseDto>> Get(QueryParameters parameters);
    public Task<PagedList<LivroResponseDto>> GetComLivros(int categoriaId, QueryParameters parameters);
    public Task<CategoriaResponseDto> GetId(int id);
    public Task<CategoriaResponseDto> Create(CategoriaDto dto);
    public Task<CategoriaResponseDto> Update(int id, CategoriaDto dto);
    public Task Delete(int id);
}
