namespace FluxoPedidos.Micro.Application.Base
{
    public interface IAplicBase
    {
        Task<ServiceResult> Recuperar();
        Task<ServiceResult> RecuperarPorId(int id);
    }
}
