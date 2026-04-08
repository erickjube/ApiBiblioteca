using ApiBiblioteca.DTOs.DtosLivro;

namespace ApiBiblioteca.DTOs.DtosAutor;

public class AutorComLivrosDto
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public DateOnly DataNascimento { get; set; }
    public string Nacionalidade { get; set; }
    public IEnumerable<LivroResumoDto> Livros { get; set; }
}
