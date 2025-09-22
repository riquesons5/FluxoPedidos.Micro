using FluxoPedidos.Micro.Application.Clientes;
using FluxoPedidos.Micro.Application.Pedidos;
using FluxoPedidos.Micro.Repository.Contexto;

namespace FluxoPedidos.Micro.Api.Configuracoes
{
    public static class InjecoesDependeciasConfig
    {
        public static IServiceCollection ResolverDependencias(this IServiceCollection services)
        {
            services.AddScoped<ContextoBanco>();

            services.AddScoped<IAplicCliente, AplicCliente>();
            services.AddScoped<IAplicPedido, AplicPedido>();

            return services;
        }
    }
}
