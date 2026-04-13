using ApiBiblioteca.Domain.ENUMs;
using ApiBiblioteca.Domain.Exceptions;

namespace ApiBiblioteca.Domain.Entities;

public class Multa
{
    public int Id { get; private set; }
    public TipoMulta Tipo {  get; private set; } 
    public decimal Valor { get; private set; } = decimal.Zero;
    public string Descricao { get; private set; }
    public  DateOnly DataMulta { get; private set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    public int EmprestimoId { get; private set; }
    public int ItemEmprestimoId { get; private set; }

    public Multa() { }

    public Multa (int emprestimoId, int itemEmprestimoId, TipoMulta tipo, decimal valor, string descricao)
    {
        if (emprestimoId <= 0) throw new BadRequestException("O Emprestimo Id deve ser um valor positivo");
        if (itemEmprestimoId <= 0) throw new BadRequestException("O ItemEmprestimo Id deve ser um valor positivo");
        if (string.IsNullOrWhiteSpace(descricao)) throw new BadRequestException("A Descrição é obrigatória");
        EmprestimoId = emprestimoId;
        ItemEmprestimoId = itemEmprestimoId;
        Tipo = tipo;
        Valor = valor;
        Descricao = descricao;
        DataMulta = DateOnly.FromDateTime(DateTime.UtcNow);
    }

    public static Multa CriarMultaPerda(int emprestimoId, int itemEmprestimoId, decimal preco)
    {
        if (preco <= 0) throw new BadRequestException("O valor do livro deve ser maior que zero");
        return new Multa(emprestimoId, itemEmprestimoId, TipoMulta.Perda, preco, "Livro não devolvido (perda)");
    }

    public static Multa CriarMultaDano(int emprestimoId, int itemEmprestimoId, decimal preco)
    {
        if (preco <= 0) throw new BadRequestException("O valor do livro deve ser maior que zero");
        return new Multa(emprestimoId, itemEmprestimoId, TipoMulta.Dano, preco, "Livro foi devolvido danificado (dano)");
    }

    public static Multa CriarMultaAtraso(int emprestimoId, int itemEmprestimoId, decimal preco)
    {
        if (preco <= 0) throw new BadRequestException("O valor do livro deve ser maior que zero");
        return new Multa(emprestimoId, itemEmprestimoId, TipoMulta.Atraso, preco, "Livro foi devolvido depois do prazo (atraso)");
    }
}

