using FluxoPedidos.Micro.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace FluxoPedidos.Micro.Repository.Contexto
{
    public class ContextoBanco : DbContext
    {
        public ContextoBanco(DbContextOptions<ContextoBanco> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureStringProperties(modelBuilder);
            ConfigureDateTimeProperties(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextoBanco).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureStringProperties(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }
        }

        private void ConfigureDateTimeProperties(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?))))
            {
                property.SetColumnType("timestamp");
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var data = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity is IDataHora))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CriadoEm").CurrentValue = data;
                    entry.Property("AtualizadoEm").CurrentValue = data;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CriadoEm").IsModified = false;
                    entry.Property("AtualizadoEm").CurrentValue = data;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
