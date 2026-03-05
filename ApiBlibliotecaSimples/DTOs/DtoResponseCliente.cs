namespace ApiBlibliotecaSimples.DTOs;

public class DtoResponseCliente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public DateOnly DataNascimento { get; set; }
    public DateOnly DataCadastro { get; set; }
}
