using ApiBiblioteca.ENUMs;

namespace ApiBiblioteca.DTOs.DtosExemplar;

public class ExemplarResumoDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public StatusExemplar Status { get; set; } 
}
