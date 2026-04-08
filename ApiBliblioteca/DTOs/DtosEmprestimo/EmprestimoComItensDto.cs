using ApiBiblioteca.DTOs.DtosItemEmprestimo;

namespace ApiBiblioteca.DTOs.DtosEmprestimo;

public class EmprestimoComItensDto
{
    public int Id { get; set; }
    public DateOnly DataEmprestimo { get; set; }
    public DateOnly PrevisaoDevolucao { get; set; }
    public string Status { get; set; }
    public int ClienteId { get; set; }
    public List<ItemEmprestimoResponseDto> Itens { get; set; } = new List<ItemEmprestimoResponseDto>();
}
