using System.ComponentModel.DataAnnotations;

namespace ApiBiblioteca.Application.DTOs.DtosLivro;

public class CreateLivroDto
{
    public string Titulo { get; set; }
    public DateOnly DataPublicacao { get; set; }
    public int NumeroDePaginas { get; set; }
    public string Isbn { get; set; }
    public long CategoriaId { get; set; }
    public long AutorId { get; set; }
}
