namespace ApiBiblioteca.DTOs.DtosAutor;

public class DtoResponseAutor
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public DateOnly DataNascimento { get; set; }
    public string Nacionalidade { get; set; }
}
