using ApiBiblioteca.Domain.ENUMs;

namespace ApiBiblioteca.Application.DTOs.DtosEmprestimo;

public class EmprestimoResumoDto
{
    public int Id { get; set; }
    public DateOnly DataEmprestimo { get; set; }
    public StatusEmprestimo Status { get; set; } 
}
