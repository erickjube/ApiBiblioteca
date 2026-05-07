using ApiBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiBiblioteca.Infrastructure.Configurations;

public class VendaConfiguration : IEntityTypeConfiguration<Venda>
{
    public void Configure(EntityTypeBuilder<Venda> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.DataVenda).IsRequired().HasColumnType("date");
        builder.Property(v => v.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Property(v => v.PrecoTotal).HasColumnType("decimal(18,2)");
        builder.HasOne(v => v.Cliente).WithMany(c => c.Vendas).HasForeignKey(v => v.ClienteId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(v => v.Itens).WithOne(i => i.Venda).HasForeignKey(i => i.VendaId).OnDelete(DeleteBehavior.Cascade);
    }
}
