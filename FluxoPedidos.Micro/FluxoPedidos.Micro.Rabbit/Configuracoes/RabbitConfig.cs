namespace FluxoPedidos.Micro.Rabbit.Configuracoes
{
    public class RabbitConfig
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FilaPedido { get; set; }
        public string FilaPedidoDeadLetter { get; set; }
    }
}
