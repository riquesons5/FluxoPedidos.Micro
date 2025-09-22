namespace FluxoPedidos.Micro.Application.Base
{
    public interface IAplicBase
    {
        ServiceResult Recuperar();
        ServiceResult RecuperarPorId(int id);
    }
}
