using ApiBiblioteca.DTOs.DtosItemVenda;

namespace ApiBiblioteca.DTOs.DtosVenda;

public class VendaComItensDto
{
    public int Id { get; set; }
    public DateOnly DataVenda { get; set; }
    public string Status { get; set; }
    public decimal? PrecoTotal { get; set; }
    public int ClienteId { get; set; }
    public List<ItemVendaResponseDto> Itens { get; set; } = new List<ItemVendaResponseDto>();
}
