namespace ApiBiblioteca.Application.DTOs.DtosItemVenda;

public class ItemVendaResponseDto
{
    public int Id { get; set; }
    public decimal Preco { get; set; }
    public string Status { get; set; }

    public int VendaId { get; set; }
    public int ExemplarId { get; set; }
}
