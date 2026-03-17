namespace ApiBiblioteca.DTOs;

public class DtoAutorComLivros
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public DateOnly DataNascimento { get; set; }
    public string Nacionalidade { get; set; }
    public IEnumerable<DtoLivroResumo> Livros { get; set; }
}
