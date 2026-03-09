using ApiBlibliotecaSimples.ENUMs;
using ApiBlibliotecaSimples.Exceptions;

namespace ApiBibliotecaSimples.Domain.Entities;

public class Emprestimo
{
    public int Id { get; private set; }
    public DateOnly DataEmprestimo { get; private set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public DateOnly PrevisaoDevolucao { get; private set; }
    public StatusEmprestimo Status { get; private set; } = StatusEmprestimo.Ativo;
    public long ClienteId { get; private set; }
    public Cliente? Cliente { get; private set; }

    public ICollection<ItemEmprestimo> Itens { get; private set; } = new List<ItemEmprestimo>();

    public Emprestimo() { }
    public Emprestimo(long clienteId)
    {
        if (clienteId <= 0)
            throw new BadRequestException("Cliente Id deve ser um valor positivo");

        DataEmprestimo = DateOnly.FromDateTime(DateTime.UtcNow);
        PrevisaoDevolucao = DataEmprestimo.AddDays(14);
        ClienteId = clienteId;
        Status = StatusEmprestimo.Ativo;
    }

    public void AdicionarItem(ExemplarLivro exemplar)
    {
        if (Status != StatusEmprestimo.Ativo) throw new BadRequestException("Empréstimo não está ativo.");
        if (Itens.Any(i => i.ExemplarId == exemplar.Id)) throw new BadRequestException("Este exemplar já foi adicionado ao empréstimo.");
        if (Itens.Count >= 3) throw new BadRequestException("Limite de itens atingido.");
        exemplar.Emprestar();
        var item = new ItemEmprestimo(exemplar.Id);
        Itens.Add(item);
    }

    public void DevolverItem(int itemId)
    {
        if (Status != StatusEmprestimo.Ativo && Status != StatusEmprestimo.Atrasado) throw new BadRequestException("Empréstimo não está ativo ou está atrasado.");
        var item = Itens.FirstOrDefault(i => i.Id == itemId);
        if (item == null) throw new BadRequestException("Item não encontrado.");
        item.Devolver();
    }

    public void MarcarItemComoPerdido(int itemId)
    {
        if (Status != StatusEmprestimo.Ativo && Status != StatusEmprestimo.Atrasado) throw new BadRequestException("Empréstimo não está ativo ou está atrasado.");
        var item = Itens.FirstOrDefault(i => i.Id == itemId);
        if (item == null) throw new BadRequestException("Item não encontrado.");
        item.MarcarComoPerdido();
    }

    public void MarcarItemComoDanificado(int itemId)
    {
        if (Status != StatusEmprestimo.Ativo && Status != StatusEmprestimo.Atrasado) throw new BadRequestException("Empréstimo não está ativo ou está atrasado.");
        var item = Itens.FirstOrDefault(i => i.Id == itemId);
        if (item == null) throw new BadRequestException("Item não encontrado.");
        item.MarcarComoDanificado();
    }

    public void Finalizar()
    {
        if (Status != StatusEmprestimo.Ativo && Status != StatusEmprestimo.Atrasado) throw new BadRequestException("Empréstimo já está finalizado ou cancelado.");
        if (Itens.Any(i => i.Status != StatusItemEmprestimo.Devolvido)) throw new BadRequestException("Ainda existem itens pendentes.");
        Status = StatusEmprestimo.Finalizado;
    }

    public void Cancelar()
    {
        if (Status != StatusEmprestimo.Ativo) throw new BadRequestException("Empréstimo já está finalizado ou cancelado.");
        if (Itens.Any()) throw new BadRequestException("Não é possível cancelar um empréstimo que possui itens.");
        Status = StatusEmprestimo.Cancelado;
    }

    public void VerificarAtraso()
    {
        if (Status == StatusEmprestimo.Ativo && DateOnly.FromDateTime(DateTime.UtcNow) > PrevisaoDevolucao)
            Status = StatusEmprestimo.Atrasado;
    }

    public void AtualizarPrevisaoDevolucao(DateOnly novaPrevisao)
    {
        if (novaPrevisao == default) throw new BadRequestException("A nova previsão de devolução é obrigatória");
        if (novaPrevisao < DataEmprestimo) throw new BadRequestException("A nova previsão de devolução não pode ser anterior à data do empréstimo.");
        if (Status != StatusEmprestimo.Ativo) throw new BadRequestException("Apenas empréstimos ativos podem ter a previsão de devolução atualizada.");
        PrevisaoDevolucao = novaPrevisao;
    }
}

