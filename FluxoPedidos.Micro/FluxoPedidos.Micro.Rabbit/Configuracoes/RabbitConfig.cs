namespace FluxoPedidos.Micro.Rabbit.Configuracoes
{
    public class RabbitConfig
    {
        public string HostName { get; set; } = "localhost";
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string FilaPedido { get; set; } = "fila_pedido";
        public string FilaPedidoDeadLetter { get; set; } = "fila_pedido.deadletter";
    }
}
