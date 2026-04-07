using ApiBiblioteca.ENUMs;

namespace ApiBiblioteca.DTOs.DtosMulta;

public class DtoResponseMulta
{
    public int Id { get; set; }
    public TipoMulta Tipo { get; set; }
    public decimal Valor { get; set; }
    public string Descricao { get; set; }
    public DateOnly DataMulta { get; set; }
    public int EmprestimoId { get; set; }
    public int ItemEmprestiomId { get; set; }
}
