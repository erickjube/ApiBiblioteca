using System.ComponentModel.DataAnnotations;

namespace ApiBlibliotecaSimples.DTOs;

public class DtoAtualizarLivro
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
