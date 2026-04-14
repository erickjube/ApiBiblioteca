using ApiBiblioteca.Application.DTOs.DtosItemVenda;
using ApiBiblioteca.Application.DTOs.DtosVenda;
using ApiBiblioteca.Application.Pagination;
using ApiBiblioteca.Domain.Common;

namespace ApiBiblioteca.Application.Interfaces.IServices;

public interface IVendaService
{
    public Task<PagedList<VendaResponseDto>> ObterVendas(QueryParameters parameters);
    public Task<VendaResponseDto> ObterVendaPorId(int vendaId);
    public Task<PagedList<ItemVendaResponseDto>> ObterVendaComItens(int vendaId, QueryParameters parameters);
    public Task<VendaResponseDto> Create(CreateVendaDto dto);
    public Task CancelarVenda(int vendaId); 
    public Task FinalizarVenda(int vendaId);
    public Task AdicionarItem(int vendaId, int exemplarId);
    public Task ExcluirItem(int vendaId, int itemId);
}
