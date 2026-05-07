namespace ApiBiblioteca.Application.DTOs.DtosLivro;

public class LivroResponseDto()
{
    public int Id { get; set; } 
    public string Titulo { get; set; } 
    public DateOnly DataPublicacao { get; set; } 
    public DateOnly DataCadastro { get; set; }
    public int NumeroDePaginas { get; set; } 
    public string Isbn { get; set; } 
    public int CategoriaId { get; set; } 
    public int AutorId { get; set; } 
}
