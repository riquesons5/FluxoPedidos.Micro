using FluxoPedidos.Micro.Domain.Base;

namespace FluxoPedidos.Micro.Domain.Models.Clientes
{
    public sealed class Cliente : ModelBase
    {
        public Cliente(string nome,
                       string documento)
        {
            Nome = nome;
            Documento = documento;
        }

        public Cliente() { }

        public string Nome { get; private set; }
        public string Documento { get; private set; }

        public void Atualizar(string nome, string documento)
        {
            Nome = nome;
            Documento = documento;
        }
    }
}
