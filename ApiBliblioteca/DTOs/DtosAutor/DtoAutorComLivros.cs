using ApiBiblioteca.DTOs.DtosLivro;

namespace ApiBiblioteca.DTOs.DtosAutor;

public class DtoAutorComLivros
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public DateOnly DataNascimento { get; set; }
    public string Nacionalidade { get; set; }
    public IEnumerable<DtoLivroResumo> Livros { get; set; }
}
