using ApiBiblioteca.Application.DTOs.DtosExemplar;
using ApiBiblioteca.Application.Pagination;
using ApiBiblioteca.Domain.Common;

namespace ApiBiblioteca.Application.Interfaces.IServices;

public interface IExemplarService
{
    public Task<PagedList<ExemplarResponseDto>> Get(QueryParameters parameters);
    public Task<ExemplarResponseDto> GetId(int id);
    public Task<ExemplarResponseDto> Create(CreateExemplarDto dto);
    public Task<ExemplarResponseDto> Update(int id, UpdateExemplarDto dto);
    public Task Delete(int id);
    public Task PerderExemplar(int id);
    public Task DanificarExemplar(int id);
}
