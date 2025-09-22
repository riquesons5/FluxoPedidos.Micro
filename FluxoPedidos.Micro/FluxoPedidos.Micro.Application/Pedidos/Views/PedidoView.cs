using FluxoPedidos.Micro.Application.Base;
using FluxoPedidos.Micro.Application.Clientes.Views;
using FluxoPedidos.Micro.Application.Pedidos.Itens.Views;
using FluxoPedidos.Micro.Domain.Models.Pedidos;

namespace FluxoPedidos.Micro.Application.Pedidos.Views
{
    public class PedidoView : ViewBase
    {
        public string NumeroPedido { get; set; }
        public int ClienteId { get; set; }
        public decimal Total { get; set; }

        public virtual ClienteView Cliente { get; set; } = null;
        public virtual ICollection<ItemView> Itens { get; set; } = [];

        public static PedidoView Map(Pedido pedido) 
        {
            return new PedidoView
            {
                Id = pedido.Id,
                NumeroPedido = pedido.NumeroPedido,
                ClienteId = pedido.ClienteId,
                Total = pedido.Total,
                Cliente = ClienteView.Map(pedido.Cliente),
                Itens = [.. pedido.Itens.Select(p => ItemView.Map(p))],
            };
        }
    }
}
