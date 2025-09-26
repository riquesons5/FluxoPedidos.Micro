using FluxoPedidos.Micro.Domain.Models.Clientes;

namespace FluxoPedidos.Micro.Application.Clientes.Validadores
{
    public interface IValidadorCliente
    {
        string? Validar(Cliente cliente);
    }
}