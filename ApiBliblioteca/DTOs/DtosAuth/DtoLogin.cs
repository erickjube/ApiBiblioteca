using System.ComponentModel.DataAnnotations;

namespace ApiBiblioteca.DTOs.DtosAuth;

public class DtoLogin
{
    [Required(ErrorMessage = "Usuario é obrigatorio!")]
    public string? Usuario { get; set; }

    [Required(ErrorMessage = "Senha é obrigatoria!")]
    public string? Senha { get; set; }
}
