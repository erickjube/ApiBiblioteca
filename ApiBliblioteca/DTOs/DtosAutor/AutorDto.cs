using System.ComponentModel.DataAnnotations;

namespace ApiBiblioteca.DTOs.DtosAutor;

public class AutorDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(200, ErrorMessage = "O nome deve conter no máximo 200 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
    public DateOnly DataNascimento { get; set; }

    [Required(ErrorMessage = "A nacionalidade é obrigatório.")]
    [StringLength(100, ErrorMessage = "A nacionalidade deve conter no máximo 100 caracteres.")]
    public string Nacionalidade { get; set; }
}
