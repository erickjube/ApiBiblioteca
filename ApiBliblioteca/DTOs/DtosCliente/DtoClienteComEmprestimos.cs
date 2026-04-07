using ApiBiblioteca.DTOs.DtosEmprestimo;

namespace ApiBiblioteca.DTOs.DtosCliente;

public class DtoClienteComEmprestimos
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public DateOnly DataNascimento { get; set; }
    public IEnumerable<DtoEmprestimoResumo> Emprestimos { get; set; }
}
