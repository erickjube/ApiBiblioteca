namespace ApiBiblioteca.Application.DTOs.DtosEmprestimo;

public class EmprestimoResponseDto
{
    public int Id { get; set; }
    public DateOnly DataEmprestimo { get; set; }
    public DateOnly PrevisaoDevolucao { get; set; }
    public string Status { get; set; } 
    public decimal MultaTotal { get; set; }
    public int ClienteId { get; set; }
}
