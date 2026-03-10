using ApiBibliotecaSimples.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ApiBlibliotecaSimples.DTOs;

public class DtoCriarItemEmprestimo
{
    [Required]
    public ExemplarLivro Item { get; set; }
}
