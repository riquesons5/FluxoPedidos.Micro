using FluxoPedidos.Micro.Application.Base;
using FluxoPedidos.Micro.Application.Pedidos.Dtos;

namespace FluxoPedidos.Micro.Application.Pedidos
{
    public class AplicPedido : IAplicPedido
    {
        public Task CriarPedidoAsync(PedidoRabbitDto dto)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Recuperar()
        {
            throw new NotImplementedException();
        }

        public ServiceResult RecuperarPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
