using FluxoPedidos.Micro.Repository.Contexto;

namespace FluxoPedidos.Micro.Api.Configuracoes
{
    public static class InjecoesDependeciasConfig
    {
        public static IServiceCollection ResolverDependencias(this IServiceCollection services)
        {
            services.AddScoped<ContextoBanco>();

            return services;
        }
    }
}
