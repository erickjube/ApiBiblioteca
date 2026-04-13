using System.ComponentModel.DataAnnotations;

namespace ApiBiblioteca.Application.DTOs.DtosExemplar;

public class CreateExemplarDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(200, ErrorMessage = "O nome deve conter no máximo 200 caracteres.")]
    public string Nome { get; set; }
    
    [Required(ErrorMessage = "O código de barras é obrigatório.")]
    [RegularExpression(@"^\d{13}$", ErrorMessage = "Código de barras inválido. Deve conter exatamente 13 dígitos.")]
    public string CodigoDeBarras { get; set; }

    [Required(ErrorMessage = "O Preco é obrigatória.")]
    [Range(1, double.MaxValue, ErrorMessage = "O Preco deve ser um valor positivo.")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "O Livro Id é obrigatório.")]
    [Range(1, long.MaxValue, ErrorMessage = "O Livro Id deve ser um valor positivo.")]
    public long LivroId { get; set; }
}
