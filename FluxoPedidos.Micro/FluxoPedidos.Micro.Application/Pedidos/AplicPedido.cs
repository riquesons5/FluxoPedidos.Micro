using FluxoPedidos.Micro.Application.Base;
using FluxoPedidos.Micro.Application.Pedidos.Dtos;
using FluxoPedidos.Micro.Application.Pedidos.Views;
using FluxoPedidos.Micro.Domain.Interfaces;
using FluxoPedidos.Micro.Domain.Models.Pedidos;

namespace FluxoPedidos.Micro.Application.Pedidos
{
    public class AplicPedido : IAplicPedido
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        public AplicPedido(IPedidoRepositorio pedidoRepositorio, 
                           IClienteRepositorio clienteRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _clienteRepositorio = clienteRepositorio;
        }
        public async Task<ServiceResult> Recuperar()
        {
            var pedidos = await _pedidoRepositorio.RecuperarTodos();

            return ServiceResult.BemSucedido(pedidos.Select(p => PedidoView.Map(p)).ToList());
        }

        public async Task<ServiceResult> RecuperarPorId(int id)
        {
            var pedido = await _pedidoRepositorio.RecuperarPorId(id);

            return ServiceResult.BemSucedido(PedidoView.Map(pedido));
        }

        public async Task<ServiceResult> Adicionar(PedidoDto pedidoDto)
        {
            var cliente = await _clienteRepositorio.RecuperarPorId(pedidoDto.CodigoCliente);

            if (cliente == null)
                return ServiceResult.Falha("Cliente não encontrado.");

            var pedido = new Pedido(cliente.Id, pedidoDto.CodigoPedido);

            foreach (var itemDto in pedidoDto.Itens)
            {
                pedido.AdicionarItem(itemDto.Produto, itemDto.Quantidade, itemDto.Preco);
            }

            pedido.Totalizar();

            await _pedidoRepositorio.Adicionar(pedido);

            return ServiceResult.BemSucedido(PedidoView.Map(pedido));
        }

        public async Task<ServiceResult> Atualizar(int id, PedidoDto pedidoDto)
        {
            var pedido = await _pedidoRepositorio.RecuperarPorId(id);

            if (pedido == null)
                return ServiceResult.Falha("Pedido não encontrado.");

            var cliente = await _clienteRepositorio.RecuperarPorId(pedidoDto.CodigoCliente);

            if (cliente == null)
                return ServiceResult.Falha("Cliente não encontrado.");

            pedido.LimparItens();

            pedido.Atualizar(cliente.Id, pedidoDto.CodigoPedido);

            foreach (var itemDto in pedidoDto.Itens)
            {
                pedido.AdicionarItem(itemDto.Produto, itemDto.Quantidade, itemDto.Preco);
            }

            pedido.Totalizar();

            await _pedidoRepositorio.Atualizar(pedido);

            return ServiceResult.BemSucedido(PedidoView.Map(pedido));
        }

        public async Task<ServiceResult> Deletar(int id)
        {
            var pedido = _pedidoRepositorio.RecuperarPorId(id);

            if(pedido == null)
                return ServiceResult.Falha("Pedido não encontrado.");

            await _pedidoRepositorio.Remover(id);

            return ServiceResult.BemSucedido();
        }

        public Task CriarPedidoAsync(PedidoDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
