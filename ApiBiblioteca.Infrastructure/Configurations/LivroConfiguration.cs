using ApiBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiBiblioteca.Infrastructure.Configurations;

public class LivroConfiguration : IEntityTypeConfiguration<Livro>
{
    public void Configure(EntityTypeBuilder<Livro> builder)
    {
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Titulo).IsRequired().HasMaxLength(200);
        builder.Property(l => l.Isbn).IsRequired().HasMaxLength(13);
        builder.HasIndex(l => l.Isbn).IsUnique();
        builder.Property(l => l.DataPublicacao).IsRequired().HasColumnType("date"); 
        builder.Property(l => l.NumeroDePaginas).IsRequired().HasColumnType("date");
        builder.HasOne(l => l.Autor).WithMany().HasForeignKey(l => l.AutorId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(l => l.Categoria).WithMany().HasForeignKey(l => l.CategoriaId).OnDelete(DeleteBehavior.Restrict);
    }
}