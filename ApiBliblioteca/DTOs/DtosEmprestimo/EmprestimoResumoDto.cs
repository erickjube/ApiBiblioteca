using ApiBiblioteca.ENUMs;

namespace ApiBiblioteca.DTOs.DtosEmprestimo;

public class EmprestimoResumoDto
{
    public int Id { get; set; }
    public DateOnly DataEmprestimo { get; set; }
    public StatusEmprestimo Status { get; set; } 
}
