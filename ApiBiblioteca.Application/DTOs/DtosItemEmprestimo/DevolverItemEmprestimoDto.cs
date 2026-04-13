using ApiBiblioteca.Domain.ENUMs;

namespace ApiBiblioteca.Application.DTOs.DtosItemEmprestimo;

public class DevolverItemEmprestimoDto
{
    public int ItemId { get; set; }
    public CondicaoItem Condicao { get; set; }
}
