namespace FluxoPedidos.Micro.Application.Base
{
    public class ServiceResult<T> where T : class
    {
        public bool Sucesso { get; set; }
        public List<string> Erros { get; private set; } = [];
        public T? Entidade { get; private set; } = null;

        public static ServiceResult<T> BemSucedido()
        {
            return new ServiceResult<T>()
            {
                Sucesso = true,
                Erros = new List<string>(),
                Entidade = null
            };
        }

        public static ServiceResult<T> BemSucedido(T entidade)
        {
            return new ServiceResult<T>()
            {
                Sucesso = true,
                Erros = new List<string>(),
                Entidade = entidade
            };
        }

        public static ServiceResult<T> Falha(string mensagem, T entidade)
        {
            return new ServiceResult<T>()
            {
                Sucesso = false,
                Erros = new List<string>() { mensagem },
                Entidade = entidade
            };
        }

        public static ServiceResult<T> Falha(List<string> erros, T entidade)
        {
            return new ServiceResult<T>()
            {
                Sucesso = false,
                Erros = erros,
                Entidade = entidade
            };
        }
    }
}
