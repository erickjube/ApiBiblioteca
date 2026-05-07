using ApiBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiBiblioteca.Infrastructure.Configurations;

public class EmprestimoConfiguration : IEntityTypeConfiguration<Emprestimo>
{
    public void Configure(EntityTypeBuilder<Emprestimo> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.DataEmprestimo).IsRequired().HasColumnType("date");
        builder.Property(e => e.PrevisaoDevolucao).IsRequired().HasColumnType("date");
        builder.Property(e => e.Status).IsRequired();
        builder.Property(e => e.MultaTotal).HasColumnType("decimal(18,2)");
        builder.HasOne(e => e.Cliente).WithMany(c => c.Emprestimos).HasForeignKey(e => e.ClienteId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(e => e.Itens).WithOne(i => i.Emprestimo).HasForeignKey(i => i.EmprestimoId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(e => e.Multas).WithOne(m => m.Emprestimo).HasForeignKey(m => m.EmprestimoId).OnDelete(DeleteBehavior.Cascade);
    }
}
