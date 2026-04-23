using System.ComponentModel.DataAnnotations;

namespace ApiBiblioteca.Application.DTOs.DtosCliente;

public class UpdateClienteDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public DateOnly DataNascimento { get; set; }
}
