using FluxoPedidos.Micro.Application.Clientes;

namespace FluxoPedidos.Micro.Api.Controllers.Clientes
{
    public class ClienteController : ControllerAplicacaoBase<IAplicCliente>
    {
        public ClienteController(IAplicCliente aplic) : base(aplic)
        {
        }
    }
}
