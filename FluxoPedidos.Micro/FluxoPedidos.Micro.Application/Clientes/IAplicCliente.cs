using FluxoPedidos.Micro.Application.Base;
using FluxoPedidos.Micro.Application.Clientes.Dtos;

namespace FluxoPedidos.Micro.Application.Clientes
{
    public interface IAplicCliente : IAplicBase
    {
        Task<ServiceResult> Adicionar(ClienteDto clienteDto);
        Task<ServiceResult> Atualizar(int id, ClienteDto clienteDto);
        Task<ServiceResult> Deletar(int id);
    }
}
