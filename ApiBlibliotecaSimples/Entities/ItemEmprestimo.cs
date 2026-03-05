using ApiBlibliotecaSimples.ENUMs;

namespace ApiBibliotecaSimples.Domain.Entities;

public class ItemEmprestimo
{
    public int Id { get; private set; }
    public DateOnly? DataDevolucao { get; private set; }
    public StatusItemEmprestimo Status { get; private set; }
    
    public int EmprestimoId { get; private set; }
    public Emprestimo Emprestimo { get; private set; }

    public long ExemplarId { get; private set; }
    public ExemplarLivro Exemplar { get; private set; }

    public ItemEmprestimo(int id, long exemplarId)
    {
        Id = id;
        ExemplarId = exemplarId;
    }

    public ItemEmprestimo(DateOnly? dataDevolucao, int emprestimoId,long exemplarId)
    {
        DataDevolucao = dataDevolucao;
        EmprestimoId = emprestimoId;
        ExemplarId = exemplarId;
    }
}
