namespace ApiBlibliotecaSimples.DTOs;

public class DtoClienteComVendas
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public DateOnly DataNascimento { get; set; }
    public IEnumerable<DtoVendaResumo> Vendas { get; set; }
}
