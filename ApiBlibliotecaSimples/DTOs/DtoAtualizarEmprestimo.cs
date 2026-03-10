using System.ComponentModel.DataAnnotations;

namespace ApiBlibliotecaSimples.DTOs;

public class DtoAtualizarEmprestimo
{
    [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
    public DateOnly PrevisaoDevolucao { get; set; }
}
