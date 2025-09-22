using FluxoPedidos.Micro.Application.Pedidos;

namespace FluxoPedidos.Micro.Api.Controllers.Pedidos
{
    public class PedidoController : ControllerAplicacaoBase<IAplicPedido>
    {
        public PedidoController(IAplicPedido aplic) : base(aplic)
        {
        }
    }
}
