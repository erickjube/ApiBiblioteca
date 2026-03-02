namespace ApiBibliotecaSimples.Domain.Entities;

public class Cliente
{
    public int Id { get; private set; }
    public string Cpf { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Telefone { get; private set; }

    public ICollection<Emprestimo> Emprestimos { get; private set; } = new List<Emprestimo>();
    public ICollection<Venda> Vendas { get; private set; } = new List<Venda>();

    public Cliente() { }
    public Cliente(string cpf, string nome, string email, string telefone)
    {
        Cpf = cpf;
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }
}
