using System.ComponentModel.DataAnnotations;

namespace ApiBiblioteca.Application.DTOs.DtosExemplar;

public class UpdateExemplarDto
{
    public string Nome { get; set; }
    public decimal Preco { get; set; }
}
