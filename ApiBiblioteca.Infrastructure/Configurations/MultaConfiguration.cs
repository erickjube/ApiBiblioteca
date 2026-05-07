using ApiBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiBiblioteca.Infrastructure.Configurations;

public class MultaConfiguration : IEntityTypeConfiguration<Multa>
{
    public void Configure(EntityTypeBuilder<Multa> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Tipo).IsRequired();
        builder.Property(m => m.Valor).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(m => m.Descricao).IsRequired().HasMaxLength(255);
        builder.Property(m => m.DataMulta).IsRequired().HasColumnType("date");
        builder.HasOne<ItemEmprestimo>().WithMany().HasForeignKey(m => m.ItemEmprestimoId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Emprestimo>().WithMany().HasForeignKey(m => m.EmprestimoId).OnDelete(DeleteBehavior.Restrict);
    }
}
