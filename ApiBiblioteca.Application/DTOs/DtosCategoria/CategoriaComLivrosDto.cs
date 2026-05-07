using ApiBiblioteca.Application.DTOs.DtosLivro;

namespace ApiBiblioteca.Application.DTOs.DtosCategoria;

public class CategoriaComLivrosDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public IEnumerable<LivroResumoDto> Livros { get; set; }
}
