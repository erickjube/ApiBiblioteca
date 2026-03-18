using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Domain.Context;

public class MeuDbContext : DbContext
{
    public MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options) { }

    public DbSet<Autor> Autor { get; set; }
    public DbSet<Categoria> Categoria { get; set; }
    public DbSet<Livro> Livro { get; set; }
    public DbSet<ExemplarLivro> Exemplar { get; set; }
    public DbSet<Emprestimo> Emprestimo { get; set; }
    public DbSet<Venda> Venda { get; set; }
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<ItemEmprestimo> ItemEmprestimo { get; set; }
    public DbSet<ItemVenda> ItemVenda { get; set; }
    public DbSet<Multa> Multa { get; set; }
}
