using ApiBiblioteca.Domain.ENUMs;
using ApiBiblioteca.Domain.Exceptions;

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

    public Multa? DevolverItem(CondicaoItem condicao, DateOnly previsaoDevolucao)
    {
        if (Status != StatusItemEmprestimo.Emprestado) throw new BadRequestException("Item não está emprestado.");
        var hoje = DateOnly.FromDateTime(DateTime.UtcNow);
        DataDevolucao = hoje;

        if (condicao == CondicaoItem.Perdido)
        {
            Status = StatusItemEmprestimo.Perdido;
            Exemplar.Perder();
            return Multa.CriarMultaPerda(EmprestimoId, Id, Exemplar.Preco);
        }

        if (condicao == CondicaoItem.Danificado)
        {
            Status = StatusItemEmprestimo.Danificado;
            Exemplar.Danificar();

            const decimal percentualDano = 0.5m;
            return Multa.CriarMultaDano(EmprestimoId, Id, Exemplar.Preco * percentualDano);
        }

        Status = StatusItemEmprestimo.Devolvido;
        Exemplar.Devolver();

        var diasAtraso = Math.Max(0, hoje.DayNumber - previsaoDevolucao.DayNumber);
        const decimal valorMultaDia = 5;
        decimal preco = diasAtraso * valorMultaDia;

        if (diasAtraso <= 0)
            return null;

        return Multa.CriarMultaAtraso(EmprestimoId, Id, preco);
    }
}
