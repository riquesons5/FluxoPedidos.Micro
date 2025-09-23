namespace FluxoPedidos.Micro.Application.Pedidos.Dtos
{
    public class PedidoDto
    {
        public string CodigoPedido { get; set; }
        public int CodigoCliente { get; set; }
        public ICollection<ItemDto> Itens { get; set; } = [];
    }
}
