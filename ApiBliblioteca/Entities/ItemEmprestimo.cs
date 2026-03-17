using ApiBiblioteca.ENUMs;
using ApiBiblioteca.Exceptions;

namespace ApiBiblioteca.Domain.Entities;

public class ItemEmprestimo
{
    public int Id { get; private set; }
    public DateOnly? DataDevolucao { get; private set; }
    public StatusItemEmprestimo Status { get; private set; } = StatusItemEmprestimo.Emprestado;

    public int EmprestimoId { get; private set; }
    public Emprestimo Emprestimo { get; private set; }

    public int ExemplarId { get; private set; }
    public ExemplarLivro Exemplar { get; private set; }

    public ItemEmprestimo(int exemplarId)
    {
        if (exemplarId <= 0) throw new BadRequestException("Exemplar Id deve ser um valor positivo");
        ExemplarId = exemplarId;
        Status = StatusItemEmprestimo.Emprestado;
    }

    public void Devolver()
    {
        if (Status != StatusItemEmprestimo.Emprestado) throw new BadRequestException("Item não está emprestado.");
        Status = StatusItemEmprestimo.Devolvido;
        DataDevolucao = DateOnly.FromDateTime(DateTime.UtcNow);
        Exemplar.Devolver();
    }

    public void MarcarComoPerdido()
    {
        if (Status != StatusItemEmprestimo.Emprestado) throw new BadRequestException("Item não está emprestado.");
        Status = StatusItemEmprestimo.Perdido;
        Exemplar.Perder();
    }

    public void MarcarComoDanificado()
    {
        if (Status != StatusItemEmprestimo.Emprestado) throw new BadRequestException("Item não está emprestado.");
        Status = StatusItemEmprestimo.Danificado;
        Exemplar.Danificar();
    }
}
