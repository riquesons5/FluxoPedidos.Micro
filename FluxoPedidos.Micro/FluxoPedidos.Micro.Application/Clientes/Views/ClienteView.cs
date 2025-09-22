using FluxoPedidos.Micro.Application.Base;
using FluxoPedidos.Micro.Application.Pedidos.Itens.Views;
using FluxoPedidos.Micro.Domain.Models.Clientes;

namespace FluxoPedidos.Micro.Application.Clientes.Views
{
    public class ClienteView : ViewBase
    {
        public string Nome { get; set; }
        public string Documento { get; set; }

        public static ClienteView Map(Cliente cliente)
        {
            return new ClienteView
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Documento = cliente.Documento
            };
        }
    }
}
