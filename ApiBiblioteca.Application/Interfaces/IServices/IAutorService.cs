using ApiBiblioteca.Application.DTOs.DtosAutor;
using ApiBiblioteca.Application.DTOs.DtosLivro;
using ApiBiblioteca.Application.Pagination;
using ApiBiblioteca.Domain.Common;

namespace ApiBiblioteca.Application.Interfaces.IServices;

public interface IAutorService
{
    public Task<PagedList<AutorResponseDto>> Get(QueryParameters parameters);
    public Task<PagedList<LivroResponseDto>> GetComLivros(long autorId, QueryParameters parameters);
    public Task<AutorResponseDto> GetId(long id);
    public Task<AutorResponseDto> Create(AutorDto dto);
    public Task<AutorResponseDto> Update(long id, AutorDto dto);
    public Task Delete(long id);
}
