using System.ComponentModel.DataAnnotations;

namespace ApiBiblioteca.DTOs.DtosCategoria;

public class CategoriaDto
{
    [Required(ErrorMessage = "O Nome é obrigatório.")]
    [StringLength(250, ErrorMessage = "O Nome deve conter no máximo 200 caracteres.")]
    public string Nome { get; set; }
}
