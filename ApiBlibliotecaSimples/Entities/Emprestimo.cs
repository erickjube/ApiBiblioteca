namespace ApiBibliotecaSimples.Domain.Entities;

public class Emprestimo
{
    public long Id { get; private set; }
    public DateOnly DataEmprestimo { get; private set; }
    public long ClienteId { get; private set; }
    public Cliente Cliente { get; private set; }

    public ICollection<ItemEmprestimo> Itens { get; private set; } = new List<ItemEmprestimo>();

    public Emprestimo() { }
    public Emprestimo(DateOnly dataEmprestimo, long clienteId)
    {
        DataEmprestimo = dataEmprestimo;
        ClienteId = clienteId;
    }
}

