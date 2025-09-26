﻿using FluxoPedidos.Micro.Application.Clientes;
using FluxoPedidos.Micro.Application.Clientes.Validadores;
using FluxoPedidos.Micro.Application.Pedidos;
using FluxoPedidos.Micro.Application.Pedidos.Validadores;
using FluxoPedidos.Micro.Domain.Interfaces;
using FluxoPedidos.Micro.Rabbit.Configuracoes;
using FluxoPedidos.Micro.Rabbit.Servicos;
using FluxoPedidos.Micro.Repository.Contexto;
using FluxoPedidos.Micro.Repository.Repositorios.Clientes;
using FluxoPedidos.Micro.Repository.Repositorios.Pedidos;
using Microsoft.Extensions.Options;

namespace FluxoPedidos.Micro.Api.Configuracoes
{
    public static class InjecoesDependeciasConfig
    {
        public static IServiceCollection ResolverDependencias(this IServiceCollection services)
        {
            services.AddScoped<ContextoBanco>();

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<RabbitConfig>>().Value);

            services.AddHostedService<RabbitServConsumidor>();

            services.AddScoped<IAplicCliente, AplicCliente>();
            services.AddScoped<IValidadorCliente, ValidadorCliente>();
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<IAplicPedido, AplicPedido>();
            services.AddScoped<IValidadorPedido, ValidadorPedido>();
            services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();

            return services;
        }
    }
}
