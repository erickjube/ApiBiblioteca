using ApiBiblioteca.Domain.ENUMs;

namespace ApiBiblioteca.Application.DTOs.DtosExemplar;

public class ExemplarResumoDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public StatusExemplar Status { get; set; } 
}
