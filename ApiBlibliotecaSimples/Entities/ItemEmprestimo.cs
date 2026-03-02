namespace ApiBibliotecaSimples.Domain.Entities;

public class ItemEmprestimo
{
    public long Id { get; private set; }
    public DateOnly PrevisaoDevolucao { get; private set; }
    public DateOnly DataDevolucao { get; private set; }
    public string Status { get; private set; }

    public long ExemplarId { get; private set; }
    public ExemplarLivro Exemplar { get; private set; }
    public ItemEmprestimo() { }
    public ItemEmprestimo(DateOnly previsaoDevolucao, DateOnly dataDevolucao, string status, long exemplarId)
    {
        PrevisaoDevolucao = previsaoDevolucao;
        DataDevolucao = dataDevolucao;
        Status = status;
        ExemplarId = exemplarId;
    }
}
