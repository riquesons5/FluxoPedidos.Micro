using FluxoPedidos.Micro.Application.Base;
using FluxoPedidos.Micro.Domain.Models.Pedidos.Itens;

namespace FluxoPedidos.Micro.Application.Pedidos.Itens.Views
{
    public class ItemView : ViewBase
    {
        public int PedidoId { get; set; }
        public string Produto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Preco { get; set; }
        public decimal Total { get; set; }

        public static ItemView Map(Item item)
        {
            return new ItemView
            {
                Id = item.Id,
                PedidoId = item.PedidoId,
                Produto = item.Produto,
                Quantidade = item.Quantidade,
                Preco = item.Preco,
                Total = item.Total
            };
        }
    }
}
