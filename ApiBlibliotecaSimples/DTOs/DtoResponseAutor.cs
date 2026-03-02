namespace ApiBlibliotecaSimples.DTOs;

public class DtoResponseAutor
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public DateOnly DataNascimento { get; set; }
    public string Nacionalidade { get; set; }
}
