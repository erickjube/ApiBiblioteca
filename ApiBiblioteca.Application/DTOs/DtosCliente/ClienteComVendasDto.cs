using ApiBiblioteca.Application.DTOs.DtosVenda;

namespace ApiBiblioteca.Application.DTOs.DtosCliente;

public class ClienteComVendasDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public DateOnly DataNascimento { get; set; }
    public IEnumerable<VendaResumoDto> Vendas { get; set; }
}
