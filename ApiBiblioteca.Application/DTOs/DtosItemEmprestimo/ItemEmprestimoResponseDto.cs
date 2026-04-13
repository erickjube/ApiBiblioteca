namespace ApiBiblioteca.Application.DTOs.DtosItemEmprestimo;

public class ItemEmprestimoResponseDto
{
    public int Id { get; set; }
    public int ExemplarId { get; set; }
    public int EmprestimoId { get; set; }
    public string Status { get; set; } 
    public DateOnly? DataDevolucao { get; set; }
}
