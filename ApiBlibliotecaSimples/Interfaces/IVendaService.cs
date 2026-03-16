using ApiBlibliotecaSimples.DTOs;

namespace ApiBlibliotecaSimples.Interfaces;

public interface IVendaService
{
    public Task<IEnumerable<DtoResponseVenda>> ObterVendas();
    public Task<DtoResponseVenda> ObterVendaPorId(int vendaId);
    public Task<DtoResponseVendaComItens> ObterVendaComItens(int vendaId);
    public Task<DtoResponseVenda> Create(DtoCriarVenda dto);
    public Task CancelarVenda(int vendaId); 
    public Task FinalizarVenda(int vendaId);
    public Task AdicionarItem(int vendaId, int exemplarId);
    public Task ExcluirItem(int vendaId, int itemId);
}
