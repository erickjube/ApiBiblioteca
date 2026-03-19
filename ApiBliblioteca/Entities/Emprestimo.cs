using ApiBiblioteca.Entities;
using ApiBiblioteca.ENUMs;
using ApiBiblioteca.Exceptions;

namespace ApiBiblioteca.Domain.Entities;

public class Emprestimo
{
    public int Id { get; private set; }
    public DateOnly DataEmprestimo { get; private set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public DateOnly PrevisaoDevolucao { get; private set; }
    public StatusEmprestimo Status { get; private set; } = StatusEmprestimo.Ativo;
    public decimal? MultaTotal { get; private set; }

    public int ClienteId { get; private set; }
    public Cliente? Cliente { get; private set; }

    public ICollection<ItemEmprestimo> Itens { get; private set; } = new List<ItemEmprestimo>();
    public ICollection<Multa> Multas { get; private set; } = new List<Multa>();


    public Emprestimo() { }
    public Emprestimo(int clienteId)
    {
        if (clienteId <= 0) throw new BadRequestException("Cliente Id deve ser um valor positivo");
        DataEmprestimo = DateOnly.FromDateTime(DateTime.UtcNow);
        PrevisaoDevolucao = DataEmprestimo.AddDays(14);
        ClienteId = clienteId;
        Status = StatusEmprestimo.Ativo;
    }

    public void AdicionarItem(int exemplarId)
    {
        if (Status != StatusEmprestimo.Ativo) throw new BadRequestException("Empréstimo não está ativo.");
        if (Itens.Any(i => i.ExemplarId == exemplarId)) throw new BadRequestException("Este exemplar já foi adicionado ao empréstimo.");
        if (Itens.Count >= 3) throw new BadRequestException("Limite de itens atingido.");
        if (exemplarId <= 0) throw new BadRequestException("Exemplar Id deve ser um valor positivo");
        var item = new ItemEmprestimo(exemplarId);
        Itens.Add(item);
    }

    public Multa? DevolverItem(int itemId, CondicaoItem condicao)
    {
        if (Status != StatusEmprestimo.Ativo) throw new BadRequestException("Empréstimo não está ativo.");
        var item = Itens.FirstOrDefault(i => i.Id == itemId);
        if (item == null) throw new BadRequestException("Item não encontrado.");
        return item.DevolverItem(condicao, PrevisaoDevolucao);
    }

    public void Finalizar()
    {
        if (Status != StatusEmprestimo.Ativo) throw new BadRequestException("Não é possivel finalizar um emprestimo que não esteja ativo");
        if (Itens.Any(i => i.Status == StatusItemEmprestimo.Emprestado)) throw new BadRequestException("Ainda existem itens pendentes.");
        Status = StatusEmprestimo.Finalizado;
    }

    public void Cancelar()
    {
        if (Status != StatusEmprestimo.Ativo) throw new BadRequestException("Empréstimo já está finalizado ou cancelado.");
        if (EstaAtrasado) throw new BadRequestException("Não é possivel cancelar um empréstimo que esta atrasado");
        if (Itens.Any()) throw new BadRequestException("Não é possível cancelar um empréstimo que possui itens.");
        Status = StatusEmprestimo.Cancelado;
    }

    // propriedade calculada, não salva no banco
    public bool EstaAtrasado =>
    Status == StatusEmprestimo.Ativo &&
    DateOnly.FromDateTime(DateTime.UtcNow) > PrevisaoDevolucao;

    public void AtualizarPrevisaoDevolucao(DateOnly novaPrevisao)
    {
        if (novaPrevisao == default) throw new BadRequestException("A nova previsão de devolução é obrigatória");
        if (novaPrevisao < DataEmprestimo) throw new BadRequestException("A nova previsão de devolução não pode ser anterior à data do empréstimo.");
        if (Status != StatusEmprestimo.Ativo) throw new BadRequestException("Apenas empréstimos ativos podem ter a previsão de devolução atualizada.");
        PrevisaoDevolucao = novaPrevisao;
    }

    public void DefinirValorMultaTotal(decimal total)
    {
        MultaTotal = total;
    }
}

