using ApiBiblioteca.ENUMs;
using ApiBiblioteca.Exceptions;

namespace ApiBiblioteca.Entities;

public class Multa
{
    public int Id { get; private set; }
    public TipoMulta Tipo {  get; private set; } 
    public decimal Valor { get; private set; } = decimal.Zero;
    public string Descricao { get; private set; }
    public  DateOnly DataMulta { get; private set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    public int EmprestimoId { get; private set; }
    public int? ItemEmprestimoId { get; private set; }

    public Multa() { }

    public Multa (int emprestimoId, int? itemEmprestimoId, string descricao)
    {
        if (emprestimoId <= 0) throw new BadRequestException("O Emprestimo Id deve ser um valor positivo");
        if (itemEmprestimoId <= 0) throw new BadRequestException("O ItemEmprestimo Id deve ser um valor positivo");
        if (string.IsNullOrWhiteSpace(descricao)) throw new BadRequestException("A Descrição é obrigatória");
        EmprestimoId = emprestimoId;
        ItemEmprestimoId = itemEmprestimoId;
        Descricao = descricao;
        DataMulta = DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
