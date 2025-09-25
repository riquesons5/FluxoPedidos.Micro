using FluxoPedidos.Micro.Application.Pedidos;
using FluxoPedidos.Micro.Application.Pedidos.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FluxoPedidos.Micro.Api.Controllers.Pedidos
{
    public class PedidoController : ControllerAplicacaoBase<IAplicPedido>
    {
        public PedidoController(IAplicPedido aplic) : base(aplic)
        {
        }

        [HttpGet("BuscarComFiltros")]
        public async Task<IActionResult> RecuperarComFiltros([FromQuery] PedidoParmetrosDto pedidoParametrosDto)
        {
            return await Executar(async () => await _aplic.Recuperar(pedidoParametrosDto));
        }

        [HttpGet("QuantidadePedidosPorCliente/{clienteId}")]
        public async Task<IActionResult> QuantidadePedidosPorCliente([FromRoute] int? clienteId)
        {
            return await Executar(async () => await _aplic.RecuperarPorClientes(clienteId));
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarPedido([FromBody] PedidoDto pedidoDto)
        {
            return await Executar(async () => await _aplic.Adicionar(pedidoDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPedido(int id, [FromBody] PedidoDto pedidoDto)
        {
            return await Executar(async () => await _aplic.Atualizar(id, pedidoDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarPedido(int id)
        {
            return await Executar(async () => await _aplic.Deletar(id));
        }
    }
}
