using ApiBiblioteca.DTOs.DtosLivro;

namespace ApiBiblioteca.DTOs.DtosCategoria;

public class CategoriaComLivrosDto
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public IEnumerable<LivroResumoDto> Livros { get; set; }
}
