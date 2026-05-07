using ApiBiblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiBiblioteca.Infrastructure.Configurations;

public class ItemVendaConfiguration : IEntityTypeConfiguration<ItemVenda>
{
    public void Configure(EntityTypeBuilder<ItemVenda> builder)
    {
        builder.HasKey(iv => iv.Id);
        builder.Property(iv => iv.Preco).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(iv => iv.Status).IsRequired();
        builder.HasOne(iv => iv.Venda).WithMany(v => v.Itens).HasForeignKey(iv => iv.VendaId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(iv => iv.Exemplar).WithMany().HasForeignKey(iv => iv.ExemplarId).OnDelete(DeleteBehavior.Restrict);
    }
}
