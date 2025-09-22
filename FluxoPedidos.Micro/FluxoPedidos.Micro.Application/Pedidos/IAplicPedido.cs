using FluxoPedidos.Micro.Application.Base;
using FluxoPedidos.Micro.Application.Pedidos.Dtos;

namespace FluxoPedidos.Micro.Application.Pedidos
{
    public interface IAplicPedido : IAplicBase
    {
        Task CriarPedidoAsync(PedidoRabbitDto dto);
    }
}
