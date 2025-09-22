
namespace FluxoPedidos.Micro.Domain.Base
{
    public class ModelBase : IDataHora
    {
        public int Id { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }
    }
}
