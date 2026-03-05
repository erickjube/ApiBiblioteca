using ApiBlibliotecaSimples.ENUMs;
using ApiBlibliotecaSimples.Exceptions;

namespace ApiBibliotecaSimples.Domain.Entities;

public class Emprestimo
{
    public int Id { get; private set; }
    public DateOnly DataEmprestimo { get; private set; }
    public DateOnly PrevisaoDevolucao { get; private set; }
    public StatusEmprestimo Status { get; private set; } = StatusEmprestimo.Ativo;
    public long ClienteId { get; private set; }
    public Cliente Cliente { get; private set; }

    public ICollection<ItemEmprestimo> Itens { get; private set; } = new List<ItemEmprestimo>();

    public Emprestimo() { }
    public Emprestimo(DateOnly dataEmprestimo, DateOnly previsaoDevolucao, long clienteId)
    {
        if (dataEmprestimo == default)
            throw new BadRequestException("Data do Emprestimo é obrigatória");
        if (dataEmprestimo > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new BadRequestException("Data do Emprestimo não pode ser futura");

        if (previsaoDevolucao == default)
            throw new BadRequestException("A previsão de devolução é obrigatória");
        if (previsaoDevolucao < dataEmprestimo)
            throw new ArgumentException("A previsão de devolução não pode ser anterior à data do empréstimo.");

        if (clienteId <= 0)
            throw new BadRequestException("Cliente Id deve ser um valor positivo");

        DataEmprestimo = dataEmprestimo;
        PrevisaoDevolucao = previsaoDevolucao;
        ClienteId = clienteId;
        Status = StatusEmprestimo.Ativo;
    }

    public void AdicionarItem(long exemplarId)
    {
        if (Status != StatusEmprestimo.Ativo)
            throw new BadRequestException("Empréstimo não está ativo.");
        if (Itens.Any(i => i.ExemplarId == exemplarId))
            throw new BadRequestException("Este exemplar já foi adicionado ao empréstimo.");

        var item = new ItemEmprestimo(this.Id, exemplarId);

        Itens.Add(item);
    }

    public void ValidarExclusao()
    {
        if (Itens.Any())
            throw new BadRequestException("Livro possui itens de Emprestimo!");
    }

    public void Finalizar()
    {
        if (Status != StatusEmprestimo.Ativo)
            throw new BadRequestException("Empréstimo já está finalizado ou cancelado.");
        if (Itens.Any(i => i.Status != StatusItemEmprestimo.Devolvido))
            throw new BadRequestException("Ainda existem itens pendentes.");
        Status = StatusEmprestimo.Finalizado;
    }

    public void Cancelar()
    {
        if (Status != StatusEmprestimo.Ativo)
            throw new BadRequestException("Empréstimo já está finalizado ou cancelado.");
        if (Itens.Any())
            throw new BadRequestException("Não é possível cancelar um empréstimo que possui itens.");
        Status = StatusEmprestimo.Cancelado;
    }

    public void AtualizarPrevisaoDevolucao(DateOnly novaPrevisao)
    {
        if (novaPrevisao == default)
            throw new BadRequestException("A nova previsão de devolução é obrigatória");
        if (novaPrevisao < DataEmprestimo)
            throw new BadRequestException("A nova previsão de devolução não pode ser anterior à data do empréstimo.");
        if (Status != StatusEmprestimo.Ativo)
            throw new BadRequestException("Apenas empréstimos ativos podem ter a previsão de devolução atualizada.");

        PrevisaoDevolucao = novaPrevisao;
    }
}

