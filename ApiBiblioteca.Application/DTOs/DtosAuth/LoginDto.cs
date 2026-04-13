using System.ComponentModel.DataAnnotations;

namespace ApiBiblioteca.Application.DTOs.DtosAuth;

public class LoginDto
{
    [Required(ErrorMessage = "Usuario é obrigatorio!")]
    public string? Usuario { get; set; }

    [Required(ErrorMessage = "Senha é obrigatoria!")]
    public string? Senha { get; set; }
}
