    using System.ComponentModel.DataAnnotations;

namespace ApiBiblioteca.Application.DTOs.DtosExemplar;

public class CreateExemplarDto
{
    public string Nome { get; set; }
    public string CodigoDeBarras { get; set; }
    public decimal Preco { get; set; }
    public long LivroId { get; set; }
}
