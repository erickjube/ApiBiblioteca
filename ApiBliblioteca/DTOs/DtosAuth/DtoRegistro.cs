using System.ComponentModel.DataAnnotations;

namespace ApiBiblioteca.DTOs.DtosAuth;

public class DtoRegistro
{
    [Required(ErrorMessage = "Usuario é obrigatorio!")]
    public string Usuario { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email é obrigatorio!")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatoria!")]
    public string Senha { get; set; }
}
