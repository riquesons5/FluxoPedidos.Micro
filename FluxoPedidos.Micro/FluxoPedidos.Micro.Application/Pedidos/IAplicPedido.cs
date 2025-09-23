using FluxoPedidos.Micro.Application.Base;
using FluxoPedidos.Micro.Application.Pedidos.Dtos;

namespace FluxoPedidos.Micro.Application.Pedidos
{
    public interface IAplicPedido : IAplicBase
    {
        Task CriarPedidoAsync(PedidoDto pedidoDto);
        Task<ServiceResult> Adicionar(PedidoDto pedidoDto);
        Task<ServiceResult> Atualizar(int id, PedidoDto pedidoDto);
        Task<ServiceResult> Deletar(int id);
    }
}
