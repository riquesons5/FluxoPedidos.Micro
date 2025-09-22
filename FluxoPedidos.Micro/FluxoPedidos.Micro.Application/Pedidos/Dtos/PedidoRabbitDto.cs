namespace FluxoPedidos.Micro.Application.Pedidos.Dtos
{
    public class PedidoRabbitDto
    {
        public string CodigoPedido { get; set; }
        public int CodigoCliente { get; set; }
        public ICollection<ItemRabbitDto> Itens { get; set; } = [];
    }
}
