using FluxoPedidos.Micro.Domain.Models.Pedidos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxoPedidos.Micro.Repository.Mappings
{
    public class PedidoMapping : MappingConfigBase<Pedido>
    {
        public override void Configure(EntityTypeBuilder<Pedido> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.NumeroPedido)
               .IsRequired();

            builder.Property(x => x.ClienteId)
               .IsRequired();

            builder.Property(x => x.Total)
               .IsRequired()
               .HasColumnType("decimal(10,4)");

            builder.HasMany(x => x.Itens)
                .WithOne(x => x.Pedido)
                .HasForeignKey(x => x.PedidoId);

            builder.ToTable("Pedidos");
        }
    }
}
