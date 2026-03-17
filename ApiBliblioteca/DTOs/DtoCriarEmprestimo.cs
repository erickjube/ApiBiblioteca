using System.ComponentModel.DataAnnotations;

namespace ApiBiblioteca.DTOs;

public class DtoCriarEmprestimo
{
    [Required(ErrorMessage = "O Cliente Id é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O Cliente Id deve ser um valor positivo.")]
    public int ClienteId { get; set; }
}
