namespace ApiBibliotecaSimples.Domain.Entities;

public class Venda
{
    public long Id { get; private set; }
    public DateOnly DataVenda { get; private set; }
    public long ClienteId { get; private set; }
    public Cliente Cliente { get; private set; }
    public ICollection<ItemVenda> Itens { get; set; } = new List<ItemVenda>();

    public Venda() { }
    public Venda(DateOnly dataVenda, long clienteId)
    {
        DataVenda = dataVenda;
        ClienteId = clienteId;
    }
}
