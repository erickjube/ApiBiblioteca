using System.ComponentModel.DataAnnotations;

namespace ApiBlibliotecaSimples.DTOs;

public class DtoAtualizarCliente
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(200, ErrorMessage = "O nome deve conter no máximo 200 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [StringLength(200, ErrorMessage = "O Email deve conter no máximo 200 caracteres.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
    [StringLength(20, ErrorMessage = "O Telefone deve conter no máximo 20 caracteres.")]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
    public DateOnly DataNascimento { get; set; }
}
