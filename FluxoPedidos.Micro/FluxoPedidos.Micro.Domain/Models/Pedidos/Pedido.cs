using FluxoPedidos.Micro.Domain.Base;
using FluxoPedidos.Micro.Domain.Models.Clientes;
using FluxoPedidos.Micro.Domain.Models.Pedidos.Itens;

namespace FluxoPedidos.Micro.Domain.Models.Pedidos
{
    public class Pedido : ModelBase
    {
        public Pedido(int clienteId,
                      string numeroPedido)
        {
            ClienteId = clienteId;
            NumeroPedido = numeroPedido;
        }

        public string NumeroPedido { get; private set; }
        public int ClienteId { get; private set; }
        public decimal Total { get; private set; } = 0;

        public Cliente Cliente { get; private set; }
        public ICollection<Item> Itens { get; private set; } = [];

        internal void Totalizar() => Total = Itens.Sum(p => p.Total).Monetario();
    }
}
