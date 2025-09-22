using FluxoPedidos.Micro.Domain.Base;

namespace FluxoPedidos.Micro.Domain.Models.Pedidos.Itens
{
    public class Item : ModelBase
    {
        public Item(int pedidoId,
                    string produto,
                    decimal quantidade,
                    decimal preco)
        {
            PedidoId = pedidoId;
            Produto = produto;
            Quantidade = quantidade;
            Preco = preco;
            Total = (Preco * Quantidade).Monetario();
        }

        public int PedidoId { get; private set; }
        public string Produto { get; private set; }
        public decimal Quantidade { get; private set; } = 0;
        public decimal Preco {  get; private set; } = 0;
        public decimal Total { get; private set; } = 0;

        public Pedido Pedido { get; private set; }
    }
}
