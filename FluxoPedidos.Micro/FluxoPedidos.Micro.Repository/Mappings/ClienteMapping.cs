using FluxoPedidos.Micro.Domain.Models.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxoPedidos.Micro.Repository.Mappings
{
    public class ClienteMapping : MappingConfigBase<Cliente>
    {
        public override void Configure(EntityTypeBuilder<Cliente> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Nome)
               .HasMaxLength(100)
               .IsRequired();

            builder.Property(x => x.Documento)
               .HasMaxLength(14)
               .IsRequired();

            builder.ToTable("Clientes");
        }
    }
}
