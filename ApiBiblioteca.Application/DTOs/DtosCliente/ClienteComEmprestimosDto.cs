using ApiBiblioteca.Application.DTOs.DtosEmprestimo;

namespace ApiBiblioteca.Application.DTOs.DtosCliente;

public class ClienteComEmprestimosDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public DateOnly DataNascimento { get; set; }
    public IEnumerable<EmprestimoResumoDto> Emprestimos { get; set; }
}
