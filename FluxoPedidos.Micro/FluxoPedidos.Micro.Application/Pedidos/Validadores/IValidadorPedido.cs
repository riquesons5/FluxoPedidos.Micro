using FluxoPedidos.Micro.Domain.Models.Pedidos;

namespace FluxoPedidos.Micro.Application.Pedidos.Validadores
{
    public interface IValidadorPedido
    {
        string? Validar(Pedido pedido);
    }
}
