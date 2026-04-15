using ApiBiblioteca.Application.DTOs.DtosCliente;
using ApiBiblioteca.Application.DTOs.DtosEmprestimo;
using ApiBiblioteca.Application.DTOs.DtosVenda;
using ApiBiblioteca.Application.Pagination;
using ApiBiblioteca.Domain.Common;

namespace ApiBiblioteca.Application.Interfaces.IServices;

public interface IClienteService
{
    public Task<PagedList<ClienteResponseDto>> Get(QueryParameters parameters);
    public Task<PagedList<EmprestimoResponseDto>> GetComEmprestimos(int clienteId, QueryParameters parameters);
    public Task<PagedList<VendaResponseDto>> GetComVendas(int clienteId, QueryParameters parameters);
    public Task<ClienteResponseDto> GetId(int clienteId);
    public Task<ClienteResponseDto> Create(CreateClienteDto dto);
    public Task<ClienteResponseDto> Update(int clienteId, UpdateClienteDto dto);
    public Task Delete(int clienteId);
}
