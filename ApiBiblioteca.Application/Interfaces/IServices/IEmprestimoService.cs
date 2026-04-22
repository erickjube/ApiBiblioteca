using ApiBiblioteca.Application.DTOs.DtosEmprestimo;
using ApiBiblioteca.Application.DTOs.DtosItemEmprestimo;
using ApiBiblioteca.Application.DTOs.DtosMulta;
using ApiBiblioteca.Application.Pagination;
using ApiBiblioteca.Domain.Common;
using ApiBiblioteca.Domain.ENUMs;

namespace ApiBiblioteca.Application.Interfaces.Services;

public interface IEmprestimoService
{
    Task<PagedList<EmprestimoResponseDto>> Get(QueryParameters parameters);
    Task<PagedList<MultaResponseDto>> GetComMultas(int emprestimoId, QueryParameters parameters);
    Task<PagedList<ItemEmprestimoResponseDto>> GetComItens(int emprestimoId, QueryParameters parameters);
    Task<EmprestimoResponseDto> GetEmprestimoById(int emprestimoId);
    Task<EmprestimoResponseDto> CreateEmprestimo(CreateEmprestimoDto dto);
    public Task<EmprestimoResponseDto> AdicionarItem(int emprestimoId, int exemplarId);
    public Task DevolverItem(int emprestimoId, DevolverItemEmprestimoDto dto);
    public Task FinalizarEmprestimo(int emprestimoId);
    public Task CancelarEmprestimo(int emprestimoId);
    public Task EstenderPrazoDevolucao(EstenderDevolucaoDto dto);
}
