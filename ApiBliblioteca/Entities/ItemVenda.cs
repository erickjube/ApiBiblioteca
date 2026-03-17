using ApiBiblioteca.ENUMs;
using ApiBiblioteca.Exceptions;

namespace ApiBiblioteca.Domain.Entities;

public class ItemVenda
{
    public int Id { get; private set; }
    public decimal Preco { get; private set; } 
    public StatusItemVenda Status { get; private set; } = StatusItemVenda.Disponivel;

    public int VendaId { get; private set; }
    public Venda Venda { get; private set; }

    public int ExemplarId { get; private set; }
    public ExemplarLivro Exemplar { get; private set; }

    public ItemVenda() { }
    public ItemVenda(int exemplarId, int vendaId)
    {
        if (vendaId <= 0) throw new BadRequestException("Venda Id deve ser um valor positivo");
        if (exemplarId <= 0) throw new BadRequestException("Exemplar Id deve ser um valor positivo");
        ExemplarId = exemplarId;
        VendaId = vendaId;
        Status = StatusItemVenda.Disponivel;
    }

    public void Vender()
    {
        if (Status != StatusItemVenda.Disponivel) throw new BadRequestException("Item não está disponível para venda.");
        Status = StatusItemVenda.Vendido;
    }

    public void Cancelar()
    {
        if (Status != StatusItemVenda.Disponivel) throw new BadRequestException("Item não pode ser cancelado.");
        Status = StatusItemVenda.Cancelado;
    }

    public bool ValidarItemVenda()
    {
        if (Status != StatusItemVenda.Cancelado && Status != StatusItemVenda.Vendido)
            return true;
        return false;
    }

    public decimal DefinirPreco(decimal preco)
    {
        Preco = preco;
        return preco;
    }
}
