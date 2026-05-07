using ApiBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiBiblioteca.Infrastructure.Configurations;

public class ExemplarConfiguration : IEntityTypeConfiguration<ExemplarLivro>
{
    public void Configure(EntityTypeBuilder<ExemplarLivro> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Nome).IsRequired().HasMaxLength(100);
        builder.Property(e => e.CodigoDeBarras).IsRequired().HasMaxLength(13);
        builder.HasIndex(e => e.CodigoDeBarras).IsUnique();
        builder.Property(e => e.Preco).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(e => e.Status).IsRequired();
        builder.HasOne(e => e.Livro).WithMany(l => l.Exemplares).HasForeignKey(e => e.LivroId).OnDelete(DeleteBehavior.Restrict);
    }
}
