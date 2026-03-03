using ApiBlibliotecaSimples.ENUMs;

namespace ApiBlibliotecaSimples.DTOs;

public class DtoExemplarResumo
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public StatusExemplar Status { get; set; } 
}
