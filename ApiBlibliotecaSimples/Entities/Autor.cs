using ApiBlibliotecaSimples.Exceptions;

namespace ApiBibliotecaSimples.Domain.Entities;

public class Autor
{
    public long Id { get; private set; }
    public string Nome { get; private set; }
    public DateOnly DataNascimento { get; private set; }
    public string Nacionalidade { get; private set; }
    public ICollection<Livro> Livros { get; private set; } = new List<Livro>();

    public Autor() { }

    public Autor(string nome, DateOnly dataNascimento, string nacionalidade)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new BadRequestException("Nome é obrigatório!");

        if (dataNascimento == default)
            throw new BadRequestException("Data de Nascimento é obrigatória!"); 

        if (string.IsNullOrWhiteSpace(nacionalidade))
            throw new BadRequestException("Nacionalidade é obrigatória!");

        Nome = nome;
        DataNascimento = dataNascimento;
        Nacionalidade = nacionalidade;
    }

    public void AtualizarInformacoes(string nome, DateOnly dataNascimento, string nacionalidade)
    {
        if (string.IsNullOrWhiteSpace(Nome))
            throw new BadRequestException("Nome inválido");

        if (dataNascimento == default)
            throw new BadRequestException("Data de Nascimento é obrigatória");

        if (dataNascimento > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new BadRequestException("Data de Nascimento não pode ser futura");

        if (string.IsNullOrWhiteSpace(Nacionalidade))
            throw new BadRequestException("Nacionalidade é obrigatória");

        Nome = nome;
        DataNascimento = dataNascimento;
        Nacionalidade = nacionalidade;
    }

    public void ValidarExclusao()
    {
        if (Livros.Any())
            throw new BadRequestException("Autor possui Livros!");
    }
}
