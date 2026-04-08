namespace ApiBiblioteca.DTOs.DtosExemplar;

public class ExemplarResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string CodigoDeBarras { get; set; }
    public string Status { get; set; }
    public decimal Preco { get; set; }
    public long LivroId { get; set; }
}
