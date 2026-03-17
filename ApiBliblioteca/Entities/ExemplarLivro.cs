using ApiBiblioteca.ENUMs;
using ApiBiblioteca.Exceptions;
using System.Text.RegularExpressions;

namespace ApiBiblioteca.Domain.Entities;

public class ExemplarLivro
{
    // mudei o id para int por que não tem necessidade de ser long
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string CodigoDeBarras { get; private set; }
    public StatusExemplar Status { get; private set; } = StatusExemplar.Disponivel;
    public decimal Preco { get; private set; }
    public long LivroId { get; private set; }
    public Livro Livro { get; private set; }

    public ExemplarLivro() { }

    public ExemplarLivro(string nome, string codigoBarras, decimal preco, long livroId)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new BadRequestException("Nome é obrigatório");
        if (!ValidarCodigoBarras.IsValid(codigoBarras)) throw new BadRequestException("Código de barras inválido");
        if (preco < 0) throw new BadRequestException("Preço deve ser um valor positivo");
        if (livroId <= 0) throw new BadRequestException("Livro Id deve ser um valor positivo");

        Nome = nome;
        CodigoDeBarras = codigoBarras;
        Preco = preco;
        LivroId = livroId;
        Status = StatusExemplar.Disponivel;
    }

    public void AtualizarInformacoes(string nome, decimal preco)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new BadRequestException("Nome é obrigatório");
        if (preco < 0) throw new BadRequestException("Preço deve ser um valor positivo");
        Nome = nome;
        Preco = preco;
    }

    public static class ValidarCodigoBarras
    {
        public static bool IsValid(string codigo)
        {
            if (!Regex.IsMatch(codigo, @"^\d{13}$")) throw new BadRequestException("Código de barras inválido.");
            return true;
        }
    }

    public void Emprestar()
    {
        if (Status != StatusExemplar.Disponivel) throw new BadRequestException("Exemplar não está disponível para empréstimo.");
        Status = StatusExemplar.Emprestado;
    }

    public void Vender() 
    {
        if (Status != StatusExemplar.Disponivel) throw new BadRequestException("Exemplar não pode ser vendido.");
        Status = StatusExemplar.Vendido;
    }

    public void Devolver()
    {
        if (Status != StatusExemplar.Emprestado) throw new BadRequestException("Exemplar não está emprestado.");
        Status = StatusExemplar.Disponivel;
    }

    public void Perder()
    {
        if (Status == StatusExemplar.Vendido) throw new BadRequestException("Exemplar vendido não pode ser perdido.");
        if (Status == StatusExemplar.Perdido) throw new BadRequestException("Exemplar já está perdido.");
        Status = StatusExemplar.Perdido;
    }

    public void Danificar()
    {
        if (Status == StatusExemplar.Vendido) throw new BadRequestException("Exemplar vendido não pode ser danificado.");
        if (Status == StatusExemplar.Danificado) throw new BadRequestException("Exemplar já está danificado.");
        Status = StatusExemplar.Danificado;
    }

    public decimal BuscarPreco()
    {
        if (Status == StatusExemplar.Vendido) throw new BadRequestException("Exemplar vendido não tem preço disponível.");
        if (Status == StatusExemplar.Perdido) throw new BadRequestException("Exemplar perdido não tem preço disponível.");
        if (Status == StatusExemplar.Danificado) throw new BadRequestException("Exemplar danificado não tem preço disponível.");
        return Preco;
    }
}
