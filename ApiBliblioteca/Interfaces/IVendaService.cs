using ApiBiblioteca.DTOs.DtosVenda;

namespace ApiBiblioteca.Interfaces;

public interface IVendaService
{
    public Task<IEnumerable<VendaResponseDto>> ObterVendas();
    public Task<VendaResponseDto> ObterVendaPorId(int vendaId);
    public Task<VendaComItensDto> ObterVendaComItens(int vendaId);
    public Task<VendaResponseDto> Create(CreateVendaDto dto);
    public Task CancelarVenda(int vendaId); 
    public Task FinalizarVenda(int vendaId);
    public Task AdicionarItem(int vendaId, int exemplarId);
    public Task ExcluirItem(int vendaId, int itemId);
}
