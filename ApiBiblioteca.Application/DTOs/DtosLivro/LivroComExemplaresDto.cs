using ApiBiblioteca.Application.DTOs.DtosExemplar;

namespace ApiBiblioteca.Application.DTOs.DtosLivro;

public class LivroComExemplaresDto
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public DateOnly DataPublicacao { get; set; }
    public DateOnly DataCadastro { get; set; }
    public int NumeroDePaginas { get; set; }
    public string Isbn { get; set; }
    public int CategoriaId { get; set; }
    public int AutorId { get; set; }
    public IEnumerable<ExemplarResumoDto> Exemplares { get; set; }
}
