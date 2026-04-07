namespace ApiBiblioteca.DTOs.DtosLivro;

public class DtoResponseLivro()
{
    public long Id { get; set; } 
    public string Titulo { get; set; } 
    public DateOnly DataPublicacao { get; set; } 
    public DateOnly DataCadastro { get; set; }
    public int NumeroDePaginas { get; set; } 
    public string Isbn { get; set; } 
    public long CategoriaId { get; set; } 
    public long AutorId { get; set; } 
}
