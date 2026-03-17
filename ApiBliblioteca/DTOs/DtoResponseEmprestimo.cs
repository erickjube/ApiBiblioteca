using ApiBiblioteca.ENUMs;

namespace ApiBiblioteca.DTOs;

public class DtoResponseEmprestimo
{
    public int Id { get; set; }
    public DateOnly DataEmprestimo { get; set; }
    public DateOnly PrevisaoDevolucao { get; set; }
    public string Status { get; set; } 
    public int ClienteId { get; set; }
}
