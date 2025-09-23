using FluxoPedidos.Micro.Application.Base;
using FluxoPedidos.Micro.Application.Clientes.Dtos;
using FluxoPedidos.Micro.Application.Clientes.Views;
using FluxoPedidos.Micro.Domain.Interfaces;
using FluxoPedidos.Micro.Domain.Models.Clientes;

namespace FluxoPedidos.Micro.Application.Clientes
{
    public class AplicCliente : IAplicCliente
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        public AplicCliente(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public async Task<ServiceResult> Recuperar()
        {
            var clientes = await _clienteRepositorio.RecuperarTodos();

            return ServiceResult.BemSucedido(clientes.Select(c => ClienteView.Map(c)).ToList());
        }

        public async Task<ServiceResult> RecuperarPorId(int id)
        {
            var cliente = await _clienteRepositorio.RecuperarPorId(id);

            return ServiceResult.BemSucedido(ClienteView.Map(cliente));
        }

        public async Task<ServiceResult> Adicionar(ClienteDto clienteDto)
        {
            var cliente = new Cliente(clienteDto.Nome, clienteDto.Documento);

            await _clienteRepositorio.Adicionar(cliente);

            return ServiceResult.BemSucedido(ClienteView.Map(cliente));
        }

        public async Task<ServiceResult> Atualizar(int id, ClienteDto clienteDto)
        {
            var cliente = await _clienteRepositorio.RecuperarPorId(id);

            if (cliente == null)
                return ServiceResult.Falha("Cliente não encontrado.");

            cliente.Atualizar(clienteDto.Nome, clienteDto.Documento);

            await _clienteRepositorio.Atualizar(cliente);

            return ServiceResult.BemSucedido(ClienteView.Map(cliente));
        }

        public async Task<ServiceResult> Deletar(int id)
        {
            var cliente = await _clienteRepositorio.RecuperarPorId(id);
            
            if (cliente == null)
                return ServiceResult.Falha("Cliente não encontrado.");

            await _clienteRepositorio.Remover(cliente.Id);

            return ServiceResult.BemSucedido();
        }
    }
}
