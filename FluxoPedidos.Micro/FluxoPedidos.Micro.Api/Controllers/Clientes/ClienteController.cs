using FluxoPedidos.Micro.Application.Clientes;
using FluxoPedidos.Micro.Application.Clientes.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FluxoPedidos.Micro.Api.Controllers.Clientes
{
    public class ClienteController : ControllerAplicacaoBase<IAplicCliente>
    {
        public ClienteController(IAplicCliente aplic) : base(aplic)
        {
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarCliente([FromBody] ClienteDto clienteDto)
        {
            return await Executar(async () => await _aplic.Adicionar(clienteDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCliente(int id, [FromBody] ClienteDto clienteDto)
        {
            return await Executar(async () => await _aplic.Atualizar(id, clienteDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarCliente(int id)
        {
            return await Executar(async () => await _aplic.Deletar(id));
        }
    }
}
