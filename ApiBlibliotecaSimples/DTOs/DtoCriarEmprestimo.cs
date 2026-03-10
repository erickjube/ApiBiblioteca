using System.ComponentModel.DataAnnotations;

namespace ApiBlibliotecaSimples.DTOs;

public class DtoCriarEmprestimo
{
    [Required(ErrorMessage = "O Exemplar Id é obrigatório.")]
    [Range(1, long.MaxValue, ErrorMessage = "O Exemplar Id deve ser um valor positivo.")]
    public int ClienteId { get; set; }
}
