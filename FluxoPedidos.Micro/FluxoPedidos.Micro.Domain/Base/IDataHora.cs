namespace FluxoPedidos.Micro.Domain.Base
{
    public interface IDataHora
    {
        DateTime CriadoEm { get; set; }
        DateTime AtualizadoEm { get; set; }
    }
}
