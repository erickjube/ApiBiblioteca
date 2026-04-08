using ApiBiblioteca.ENUMs;

namespace ApiBiblioteca.DTOs.DtosItemEmprestimo;

public class DevolverItemEmprestimoDto
{
    public int ItemId { get; set; }
    public CondicaoItem Condicao { get; set; }
}
