namespace ApiBiblioteca.DTOs;

public class DtoCategoriaComLivros
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public IEnumerable<DtoLivroResumo> Livros { get; set; }
}
