namespace FluxoPedidos.Micro.Application.Pedidos.Dtos
{
    public class PedidoParmetrosDto
    {
        public int? ClienteId { get; set; }
        public string? NumeroPedido { get; set; }
        public string? NomeCliente { get; set; }
        public int? ItemId { get; set; }
        public string? Produto { get; set; }
    }
}
