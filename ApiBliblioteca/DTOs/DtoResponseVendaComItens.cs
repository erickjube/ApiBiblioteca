namespace ApiBiblioteca.DTOs;

public class DtoResponseVendaComItens
{
    public int Id { get; set; }
    public DateOnly DataVenda { get; set; }
    public string Status { get; set; }
    public decimal? PrecoTotal { get; set; }
    public int ClienteId { get; set; }
    public List<DtoResponseItemVenda> Itens { get; set; } = new List<DtoResponseItemVenda>();
}
