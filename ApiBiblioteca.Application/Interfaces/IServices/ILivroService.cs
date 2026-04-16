using ApiBiblioteca.Application.DTOs.DtosExemplar;
using ApiBiblioteca.Application.DTOs.DtosLivro;
using ApiBiblioteca.Application.Pagination;
using ApiBiblioteca.Domain.Common;

namespace ApiBiblioteca.Application.Interfaces.IServices;

public interface ILivroService
{
    public Task<PagedList<LivroResponseDto>> Get(QueryParameters parameters);
    public Task<PagedList<ExemplarResponseDto>> GetComExemplares(long livroId, QueryParameters parameters);
    public Task<LivroResponseDto> GetId(long livroId);
    public Task<LivroResponseDto> Create(CreateLivroDto dto);
    public Task<LivroResponseDto> Update(long livroId, UpdateLivroDto dto);
    public Task Delete(long livroId);
}
