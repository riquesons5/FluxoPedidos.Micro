using FluxoPedidos.Micro.Application.Base;
using FluxoPedidos.Micro.Application.Pedidos.Dtos;
using FluxoPedidos.Micro.Application.Pedidos.Views;
using FluxoPedidos.Micro.Domain.Interfaces;
using FluxoPedidos.Micro.Domain.Models.Pedidos;
using System.Linq.Expressions;

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

            return ServiceResult.BemSucedido(pedidos.Select(PedidoView.Map).ToList());
        }

        public async Task<ServiceResult> Recuperar(PedidoParmetrosDto pedidoParametrosDto)
        {
            Expression<Func<Pedido, bool>> filtro = p => true;

            if (pedidoParametrosDto.ClienteId.HasValue)
                filtro = E(filtro, p => p.ClienteId == pedidoParametrosDto.ClienteId.Value);

            if (!string.IsNullOrWhiteSpace(pedidoParametrosDto.NumeroPedido))
                filtro = E(filtro, p => p.NumeroPedido == pedidoParametrosDto.NumeroPedido);

            if (!string.IsNullOrWhiteSpace(pedidoParametrosDto.NomeCliente))
                filtro = E(filtro, p => p.Cliente.Nome.ToUpper().Contains(pedidoParametrosDto.NomeCliente.ToUpper()));

            if (pedidoParametrosDto.ItemId.HasValue)
                filtro = E(filtro, p => p.Itens.Any(i => i.Id == pedidoParametrosDto.ItemId.Value));

            if (!string.IsNullOrWhiteSpace(pedidoParametrosDto.Produto))
                filtro = E(filtro, p => p.Itens.Any(i => i.Produto.ToUpper().Contains(pedidoParametrosDto.Produto.ToUpper())));

            var pedidos = await _pedidoRepositorio.Buscar(filtro);

            return ServiceResult.BemSucedido(pedidos.Select(PedidoView.Map).ToList());
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

            var pedido = new Pedido(cliente.Id, pedidoDto.CodigoPedido.ToString());

            foreach (var itemDto in pedidoDto.Itens)
            {
                pedido.AdicionarItem(itemDto.Produto, itemDto.Quantidade, itemDto.Preco);
            }

            pedido.Totalizar();

            await _pedidoRepositorio.Adicionar(pedido);

            pedido.DefinirCliente(cliente);

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

            pedido.Atualizar(cliente.Id, pedidoDto.CodigoPedido.ToString());

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

            if (pedido == null)
                return ServiceResult.Falha("Pedido não encontrado.");

            await _pedidoRepositorio.Remover(id);

            return ServiceResult.BemSucedido();
        }

        public async Task CriarPedidoAsync(PedidoDto dto)
        {
            var resultado = await Adicionar(dto);

            if (!resultado.Sucesso)
                throw new Exception($"Erro ao inserir pedido: {resultado.ToString()}");
        }

        private static Expression<Func<T, bool>> E<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var body = Expression.AndAlso(
                Expression.Invoke(expr1, parameter),
                Expression.Invoke(expr2, parameter));

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public async Task<ServiceResult> RecuperarPorClientes(int? clienteId)
        {
            var pedidos = clienteId.HasValue ? 
                await _pedidoRepositorio.Buscar(p => p.ClienteId == clienteId) :
                await _pedidoRepositorio.Buscar(p => true);

            if (!pedidos.Any())
                return ServiceResult.Falha("Nenhum pedido encontrado.");

            var retornoLista = pedidos
                .GroupBy(p => new { p.ClienteId, p.Cliente.Nome })
                .Select(g => new
                {
                    ClienteId = g.Key.ClienteId,
                    NomeCliente = g.Key.Nome,
                    QuantidadePedidos = g.Count(),
                    TotalPedidos = g.Sum(p => p.Total),
                    Pedidos = g.Select(p => new
                    {
                        NumeroPedido = p.NumeroPedido,
                        Total = p.Total
                    }).ToList()
                }).ToList();

            return ServiceResult.BemSucedido(retornoLista);
        }
    }
}
