namespace ApiBiblioteca.DTOs;

public class DtoResponseEmprestimoComMultas
{
    public int Id { get; set; }
    public DateOnly DataEmprestimo { get; set; }
    public DateOnly PrevisaoDevolucao { get; set; }
    public string Status { get; set; }
    public int ClienteId { get; set; }
    public List<DtoResponseMulta> Multas { get; set; } = new List<DtoResponseMulta>();
}
