using System.ComponentModel.DataAnnotations;

namespace ApiBiblioteca.DTOs.DtosLivro;

public class CreateLivroDto
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(250, ErrorMessage = "O título deve conter no máximo 250 caracteres.")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "A data de publicação é obrigatória.")]
    public DateOnly DataPublicacao { get; set; }

    [Required(ErrorMessage = "O número de páginas é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O número de páginas deve ser maior que zero.")]
    public int NumeroDePaginas { get; set; }

    [Required(ErrorMessage = "O ISBN é obrigatório.")]
    [RegularExpression(@"^(97(8|9))?\d{9}(\d|X)$", ErrorMessage = "ISBN inválido")]
    public string Isbn { get; set; }

    [Required(ErrorMessage = "A Categoria Id é obrigatória.")]
    [Range(1, long.MaxValue, ErrorMessage = "A Categoria Id deve ser um valor positivo.")]
    public long CategoriaId { get; set; }

    [Required(ErrorMessage = "O Autor Id é obrigatório.")]
    [Range(1, long.MaxValue, ErrorMessage = "O Autor Id deve ser um valor positivo.")]
    public long AutorId { get; set; }
}
