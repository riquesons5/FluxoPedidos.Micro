using FluxoPedidos.Micro.Application.Base;
using FluxoPedidos.Micro.Application.Clientes.Dtos;
using FluxoPedidos.Micro.Application.Clientes.Validadores;
using FluxoPedidos.Micro.Application.Clientes.Views;
using FluxoPedidos.Micro.Domain.Interfaces;
using FluxoPedidos.Micro.Domain.Models.Clientes;

namespace FluxoPedidos.Micro.Application.Clientes
{
    public class AplicCliente : IAplicCliente
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IValidadorCliente _validadorCliente;
        public AplicCliente(IClienteRepositorio clienteRepositorio, 
                            IValidadorCliente validadorCliente)
        {
            _clienteRepositorio = clienteRepositorio;
            _validadorCliente = validadorCliente;
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

            var validar = _validadorCliente.Validar(cliente);

            if(validar != null)
                return ServiceResult.Falha(validar);

            await _clienteRepositorio.Adicionar(cliente);

            return ServiceResult.BemSucedido(ClienteView.Map(cliente));
        }

        public async Task<ServiceResult> Atualizar(int id, ClienteDto clienteDto)
        {
            var cliente = await _clienteRepositorio.RecuperarPorId(id);

            if (cliente == null)
                return ServiceResult.Falha("Cliente não encontrado.");

            cliente.Atualizar(clienteDto.Nome, clienteDto.Documento);

            var validar = _validadorCliente.Validar(cliente);

            if (validar != null)
                return ServiceResult.Falha(validar);

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
