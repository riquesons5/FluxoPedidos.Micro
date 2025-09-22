
using System.ComponentModel.DataAnnotations;

namespace FluxoPedidos.Micro.Domain.Base
{
    public class ModelBase : IDataHora
    {
        public ModelBase()
        {
            CriadoEm = DateTime.UtcNow;
            AtualizadoEm = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }
    }
}
