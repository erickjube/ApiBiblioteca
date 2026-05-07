using ApiBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiBiblioteca.Infrastructure.Configurations;

public class ItemEmprestimoConfiguration : IEntityTypeConfiguration<ItemEmprestimo>
{
    public void Configure(EntityTypeBuilder<ItemEmprestimo> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.DataDevolucao).HasColumnType("date");
        builder.Property(i => i.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.HasOne(i => i.Emprestimo).WithMany(e => e.Itens).HasForeignKey(i => i.EmprestimoId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(i => i.Exemplar).WithMany().HasForeignKey(i => i.ExemplarId).OnDelete(DeleteBehavior.Restrict);
    }
}