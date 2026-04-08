using ApiBiblioteca.DTOs.DtosMulta;

namespace ApiBiblioteca.DTOs.DtosEmprestimo;

public class EmprestimoComMultasDto
{
    public int Id { get; set; }
    public DateOnly DataEmprestimo { get; set; }
    public DateOnly PrevisaoDevolucao { get; set; }
    public string Status { get; set; }
    public int ClienteId { get; set; }
    public List<MultaResponseDto> Multas { get; set; } = new List<MultaResponseDto>();
}
