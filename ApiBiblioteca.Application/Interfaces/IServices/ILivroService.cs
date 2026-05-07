using ApiBiblioteca.Application.DTOs.DtosExemplar;
using ApiBiblioteca.Application.DTOs.DtosLivro;
using ApiBiblioteca.Application.Pagination;
using ApiBiblioteca.Domain.Common;

namespace ApiBiblioteca.Application.Interfaces.IServices;

public interface ILivroService
{
    public Task<PagedList<LivroResponseDto>> Get(QueryParameters parameters);
    public Task<PagedList<ExemplarResponseDto>> GetComExemplares(int livroId, QueryParameters parameters);
    public Task<LivroResponseDto> GetId(int livroId);
    public Task<LivroResponseDto> Create(CreateLivroDto dto);
    public Task<LivroResponseDto> Update(int livroId, UpdateLivroDto dto);
    public Task Delete(int livroId);
}
