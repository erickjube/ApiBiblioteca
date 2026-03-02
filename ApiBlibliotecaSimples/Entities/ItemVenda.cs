namespace ApiBibliotecaSimples.Domain.Entities;

public class ItemVenda
{
    public long Id { get; private set; }
    public int Quantidade { get; private set; }
    public decimal PrecoUnitario { get; private set; }

    public long ExemplarId { get; private set; }
    public ExemplarLivro Exemplar { get; private set; }

    public ItemVenda() { }
    public ItemVenda(int quantidade, decimal precoUnitario, long exemplarId)
    {
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
        ExemplarId = exemplarId;
    }
}
