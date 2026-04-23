using System.ComponentModel.DataAnnotations;

namespace ApiBiblioteca.Application.DTOs.DtosAutor;

public class AutorDto
{
    public string Nome { get; set; }
    public DateOnly DataNascimento { get; set; }
    public string Nacionalidade { get; set; }
}
