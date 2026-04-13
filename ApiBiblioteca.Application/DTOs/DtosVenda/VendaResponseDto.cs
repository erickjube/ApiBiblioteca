namespace ApiBiblioteca.Application.DTOs.DtosVenda;

public class VendaResponseDto
{
    public int Id { get; set; }
    public DateOnly DataVenda { get; set; }
    public string Status { get; set; }
    public decimal? PrecoTotal { get; set; }
    public int ClienteId { get; set; }
}
