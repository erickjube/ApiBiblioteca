using ApiBiblioteca.ENUMs;

namespace ApiBiblioteca.DTOs.DtosItemEmprestimo;

public class DtoDevolverItemRequest
{
    public int ItemId { get; set; }
    public CondicaoItem Condicao { get; set; }
}
