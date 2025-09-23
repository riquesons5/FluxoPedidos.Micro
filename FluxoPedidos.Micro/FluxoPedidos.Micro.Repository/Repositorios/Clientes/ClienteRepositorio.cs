using FluxoPedidos.Micro.Domain.Interfaces;
using FluxoPedidos.Micro.Domain.Models.Clientes;
using FluxoPedidos.Micro.Repository.Contexto;

namespace FluxoPedidos.Micro.Repository.Repositorios.Clientes
{
    public class ClienteRepositorio : Repositorio<Cliente>, IClienteRepositorio
    {
        public ClienteRepositorio(ContextoBanco dbContexto) : base(dbContexto)
        {
        }
    }
}
