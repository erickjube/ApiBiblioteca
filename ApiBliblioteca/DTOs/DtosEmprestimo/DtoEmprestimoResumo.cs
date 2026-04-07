using ApiBiblioteca.ENUMs;

namespace ApiBiblioteca.DTOs.DtosEmprestimo;

public class DtoEmprestimoResumo
{
    public int Id { get; set; }
    public DateOnly DataEmprestimo { get; set; }
    public StatusEmprestimo Status { get; set; } 
}
