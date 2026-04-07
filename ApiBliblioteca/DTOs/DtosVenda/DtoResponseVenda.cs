namespace ApiBiblioteca.DTOs.DtosVenda;

public class DtoResponseVenda
{
    public int Id { get; set; }
    public DateOnly DataVenda { get; set; }
    public string Status { get; set; }
    public decimal? PrecoTotal { get; set; }
    public int ClienteId { get; set; }
}
