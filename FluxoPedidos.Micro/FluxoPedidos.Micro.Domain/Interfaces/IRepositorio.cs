using FluxoPedidos.Micro.Domain.Base;

namespace FluxoPedidos.Micro.Domain.Interfaces
{
    public interface IRepositorio<T> : IDisposable where T : ModelBase
    {

    }
}
