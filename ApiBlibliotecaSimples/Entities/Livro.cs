using ApiBlibliotecaSimples.Exceptions;

namespace ApiBibliotecaSimples.Domain.Entities;

public class Livro
{
    public long Id { get; private set; }
    public string Titulo { get; private set; }
    public DateOnly DataPublicacao { get; private set; }
    public DateOnly DataCadastro { get; private set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public int NumeroDePaginas { get; private set; }
    public string Isbn { get; private set; }

    public long CategoriaId { get; private set; }
    public Categoria Categoria { get; private set; }

    public long AutorId { get; private set; }
    public Autor Autor { get; private set; }

    public ICollection<ExemplarLivro> Exemplares { get; private set; } = new List<ExemplarLivro>();

    private Livro() { }

    public Livro(string titulo, DateOnly dataPublicacao, int numeroPaginas, string isbn, long categoriaId, long autorId)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new BadRequestException("Título é obrigatório");

        if (dataPublicacao == default)
            throw new BadRequestException("Data de publicação é obrigatória");

        if (dataPublicacao > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new BadRequestException("Data de publicação não pode ser futura");

        if (numeroPaginas <= 0)
            throw new BadRequestException("Número de páginas deve ser maior que zero");

        if (!IsbnHelper.IsValid(isbn))
            throw new BadRequestException("ISBN inválido");

        if (categoriaId <= 0)
            throw new BadRequestException("Categoria Id deve ser um valor positivo");

        if (autorId <= 0)
            throw new BadRequestException("Autor Id deve ser um valor positivo");

        Titulo = titulo;
        DataPublicacao = dataPublicacao;
        DataCadastro = DateOnly.FromDateTime(DateTime.UtcNow);
        NumeroDePaginas = numeroPaginas;
        Isbn = isbn;
        CategoriaId = categoriaId;
        AutorId = autorId;
    }

    public static class IsbnHelper
    {
        public static bool IsValid(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                return false;

            isbn = isbn.Replace("-", "").Replace(" ", "");

            return isbn.Length switch
            {
                10 => IsValidIsbn10(isbn),
                13 => IsValidIsbn13(isbn),
                _ => false
            };
        }

        private static bool IsValidIsbn10(string isbn)
        {
            if (!isbn.Take(9).All(char.IsDigit))
                return false;

            int sum = 0;

            for (int i = 0; i < 9; i++)
                sum += (isbn[i] - '0') * (10 - i);

            int remainder = sum % 11;
            int check = remainder == 0 ? 0 : 11 - remainder;

            if (check == 10)
                return isbn[9] == 'X';

            return isbn[9] - '0' == check;
        }

        private static bool IsValidIsbn13(string isbn)
        {
            if (!isbn.All(char.IsDigit))
                return false;

            int sum = 0;

            for (int i = 0; i < 12; i++)
            {
                int digit = isbn[i] - '0';
                sum += (i % 2 == 0) ? digit : digit * 3;
            }

            int check = (10 - (sum % 10)) % 10;

            return isbn[12] - '0' == check;
        }
    }

    public void AtualizarInformacoes(string titulo, int numeroPaginas, DateOnly dataPublicacao, long categoriaId)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new BadRequestException("Título inválido");

        if (numeroPaginas <= 0)
            throw new BadRequestException("Número de páginas inválido");

        if (dataPublicacao == default)
            throw new BadRequestException("Data de publicação é obrigatória");

        if (dataPublicacao > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new BadRequestException("Data de publicação não pode ser futura");

        if (categoriaId <= 0)
            throw new BadRequestException("Categoria inválida");

        Titulo = titulo;
        NumeroDePaginas = numeroPaginas;
        DataPublicacao = dataPublicacao;
        CategoriaId = categoriaId;
    }

    public void ValidarExclusao()
    {
        if (Exemplares.Any())
            throw new BadRequestException("Livro possui exemplares!");
    }
}
