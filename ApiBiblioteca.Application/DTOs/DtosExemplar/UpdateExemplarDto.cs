using System.ComponentModel.DataAnnotations;

namespace ApiBiblioteca.Application.DTOs.DtosExemplar;

public class UpdateExemplarDto
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(200, ErrorMessage = "O nome deve conter no máximo 200 caracteres.")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O campo Preço é obrigatório.")]
    [Range(0, double.MaxValue, ErrorMessage = "O campo Preço deve ser um valor positivo.")]
    public decimal Preco { get; set; }
}
