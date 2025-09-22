using FluxoPedidos.Micro.Domain.Models.Pedidos.Itens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxoPedidos.Micro.Repository.Mappings
{
    internal class ItemMapping : MappingConfigBase<Item>
    {
        public override void Configure(EntityTypeBuilder<Item> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.PedidoId)
               .IsRequired();

            builder.Property(x => x.Produto)
               .IsRequired();

            builder.Property(x => x.Quantidade)
               .IsRequired()
               .HasColumnType("decimal(10,4)");

            builder.Property(x => x.Preco)
               .IsRequired()
               .HasColumnType("decimal(10,4)");

            builder.Property(x => x.Total)
               .IsRequired()
               .HasColumnType("decimal(10,4)");

            builder.ToTable("Itens");
        }
    }
}
