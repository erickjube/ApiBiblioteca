using ApiBiblioteca.ENUMs;
using ApiBiblioteca.Exceptions;

namespace ApiBiblioteca.Domain.Entities;

public class Cliente
{
    public int Id { get; private set; }
    public string Cpf { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Telefone { get; private set; }
    public DateOnly DataNascimento { get; private set; }
    public DateOnly DataCadastro { get; private set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    public ICollection<Emprestimo> Emprestimos { get; private set; } = new List<Emprestimo>();
    public ICollection<Venda> Vendas { get; private set; } = new List<Venda>();

    public Cliente() { }
    public Cliente(string cpf, string nome, string email, string telefone, DateOnly dataNascimento)
    {
        if (string.IsNullOrWhiteSpace(cpf)) throw new BadRequestException("CPF é obrigatório.");
        if (!CpfValido(cpf)) throw new BadRequestException("CPF inválido.");
        if (string.IsNullOrWhiteSpace(nome)) throw new BadRequestException("Nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(email)) throw new BadRequestException("Email é obrigatório.");
        if (!EmailValido(email)) throw new BadRequestException("Email inválido.");
        if (string.IsNullOrWhiteSpace(telefone)) throw new BadRequestException("Telefone é obrigatório.");
        if (!TelefoneValido(telefone)) throw new BadRequestException("Telefone inválido.");
        if (dataNascimento == default) throw new BadRequestException("Data de Nascimento é obrigatória!");
        if (dataNascimento > DateOnly.FromDateTime(DateTime.UtcNow)) throw new BadRequestException("Data de nascimento não pode ser futura");

        Cpf = cpf;
        Nome = nome;
        Email = email;
        Telefone = telefone;
        DataNascimento = dataNascimento;
        DataCadastro = DateOnly.FromDateTime(DateTime.UtcNow);
    }

    private static bool CpfValido(string cpf)
    {
        // transforma o CPF para conter apenas dígitos
        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        // CPF deve conter 11 dígitos
        if (cpf.Length != 11) return false;

        // Verifica se todos os dígitos são iguais (ex: 111.111.111-11)
        if (cpf.Distinct().Count() == 1) return false;

        // Cálculo dos dígitos verificadores
        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        // Calcula o primeiro dígito verificador
        string tempCpf = cpf[..9];
        int soma = 0;

        // Multiplica cada dígito pelos multiplicadores e soma os resultados
        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        // Calcula o resto da divisão por 11
        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        // Concatena o primeiro dígito verificador ao CPF temporário
        string digito = resto.ToString();
        tempCpf += digito;
        soma = 0;
        
        // Calcula o segundo dígito verificador
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        digito += resto.ToString();

        // Verifica se os dígitos calculados correspondem aos dígitos do CPF
        return cpf.EndsWith(digito);
    }

    private static bool EmailValido(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private static bool TelefoneValido(string telefone)
    {
        telefone = new string(telefone.Where(char.IsDigit).ToArray());

        return telefone.Length == 10 || telefone.Length == 11;
    }

    public void AtualizarInformacoes(string nome, string email, string telefone)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new BadRequestException("Nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(email)) throw new BadRequestException("Email é obrigatório.");
        if (!EmailValido(email)) throw new BadRequestException("Email inválido.");
        if (string.IsNullOrWhiteSpace(telefone)) throw new BadRequestException("Telefone é obrigatório.");
        if (!TelefoneValido(telefone)) throw new BadRequestException("Telefone inválido.");
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }

    public void ValidarExclusao()
    {
        if (Emprestimos.Any(e => e.Status == StatusEmprestimo.Ativo)) throw new BadRequestException("Cliente possui empréstimos ativos!");
        if (Vendas.Any()) throw new BadRequestException("Cliente possui vendas registradas!");
    }
}
