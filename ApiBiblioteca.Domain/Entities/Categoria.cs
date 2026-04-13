using ApiBiblioteca.Domain.Exceptions;

namespace ApiBiblioteca.Domain.Entities;

public class Categoria
{
    public long Id { get; private set; }
    public string Nome { get; private set; }
    public ICollection<Livro> Livros { get; private set; } = new List<Livro>();

    public Categoria() { }

    public Categoria(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new BadRequestException("Nome é obrigatório");
        Nome = nome;
    }

    public void AtualizarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new BadRequestException("Nome inválido");

        Nome = nome;
    }

    public void ValidarExclusao()
    {
        if (Livros.Any())
            throw new BadRequestException("Categoria possui livros");
    }
}
