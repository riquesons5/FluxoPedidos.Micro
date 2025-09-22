using FluxoPedidos.Micro.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxoPedidos.Micro.Repository.Mappings
{
    public class MappingConfigBase<T> : IEntityTypeConfiguration<T> where T : ModelBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(e => e.CriadoEm)
                   .IsRequired();

            builder.Property(e => e.AtualizadoEm)
                   .IsRequired();
        }
    }
}
