using ApiBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ApiBiblioteca.Infrastructure.Configurations;

public class AutorConfiguration : IEntityTypeConfiguration<Autor>
{
    public void Configure(EntityTypeBuilder<Autor> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Nome).IsRequired().HasMaxLength(100);
        builder.Property(a => a.DataNascimento).IsRequired().HasColumnType("date");
        builder.Property(a => a.Nacionalidade).IsRequired().HasMaxLength(50);
        builder.HasMany(a => a.Livros).WithOne(l => l.Autor).HasForeignKey(l => l.AutorId).OnDelete(DeleteBehavior.Restrict);
    }
}
