using ApiBiblioteca.ENUMs;
using ApiBiblioteca.Exceptions;

namespace ApiBiblioteca.Domain.Entities;

public class Venda
{
    public int Id { get; private set; }
    public DateOnly DataVenda { get; private set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public StatusVenda Status { get; private set; } = StatusVenda.Aberta;
    public decimal? PrecoTotal { get; private set; } 
    public int ClienteId { get; private set; }
    public Cliente Cliente { get; private set; }
    public ICollection<ItemVenda> Itens { get; private set; } = new List<ItemVenda>();

    public Venda() { }
    public Venda(int clienteId)
    {
        if (clienteId <= 0) throw new BadRequestException("Cliente Id deve ser um valor positivo");
        DataVenda = DateOnly.FromDateTime(DateTime.UtcNow);
        Status = StatusVenda.Aberta;
        ClienteId = clienteId;
    }

    public ItemVenda AdicionarItem(ExemplarLivro exemplar)
    {
        if (Status != StatusVenda.Aberta) throw new BadRequestException("Venda não está aberta.");
        if (exemplar.Status != StatusExemplar.Disponivel) throw new BadRequestException("Exemplar não esta disponivel.");
        var item = new ItemVenda(exemplar.Id, Id);
        if (Itens.Any(i => i.ExemplarId == exemplar.Id)) throw new BadRequestException("Este exemplar já foi adicionado à venda.");
        Itens.Add(item);
        return item;
    }

    public void ExcluirItem(int itemId)
    {
        if (Status != StatusVenda.Aberta) throw new BadRequestException("Venda não está aberta.");
        var item = Itens.FirstOrDefault(i => i.Id == itemId);
        if (item == null) throw new BadRequestException("Item não encontrado na venda.");
        item.Cancelar();
        Itens.Remove(item);
    }   

    public void Finalizar()
    {
        if (Status != StatusVenda.Aberta) throw new BadRequestException("Venda já finalizada");
        if (!Itens.Any()) throw new BadRequestException("Venda precisa ter itens");
        Status = StatusVenda.Finalizada;
    }

    public void Cancelar()
    {
        if (Status != StatusVenda.Aberta) throw new BadRequestException("Venda já finalizada ou cancelada.");
        Status = StatusVenda.Cancelada;
    }

    public bool ValidarVenda()
    {
        if (Status == StatusVenda.Aberta)
            return true;
        return false;
    }

    public void DefinirPrecoTotal(decimal? precoTotal)
    {
        PrecoTotal = precoTotal;
    }
}
