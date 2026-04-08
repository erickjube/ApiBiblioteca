using System.ComponentModel.DataAnnotations;

namespace ApiBiblioteca.DTOs.DtosLivro;

public class UpdateLivroDto
{
    [Required]
    public string Titulo { get; set; }
    [Required]
    public DateOnly DataPublicacao { get; set; }

    [Range(1, int.MaxValue)]
    public int NumeroDePaginas { get; set; }

    [Range(1, long.MaxValue)]
    public long CategoriaId { get; set; }
}
