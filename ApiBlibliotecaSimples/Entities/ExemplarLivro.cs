using ApiBibliotecaSimples.Domain.Entities;

namespace ApiBibliotecaSimples.Domain.Entities;

public class ExemplarLivro
{
    public long Id { get; private set; }
    public string CodigoBarras { get; private set; }
    public bool Disponivel { get; private set; }
    public decimal Preco { get; private set; }

    public long LivroId { get; private set; }
    public Livro Livro { get; private set; }

    public ExemplarLivro() { }

    public ExemplarLivro(string codigoBarras, bool disponivel, decimal preco, long livroId)
    {
        CodigoBarras = codigoBarras;
        Disponivel = disponivel;
        Preco = preco;
        LivroId = livroId;
    }
}
