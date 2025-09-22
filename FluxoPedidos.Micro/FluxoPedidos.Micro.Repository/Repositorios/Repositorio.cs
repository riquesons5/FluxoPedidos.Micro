using FluxoPedidos.Micro.Domain.Base;
using FluxoPedidos.Micro.Domain.Interfaces;
using FluxoPedidos.Micro.Repository.Contexto;
using Microsoft.EntityFrameworkCore;

namespace FluxoPedidos.Micro.Repository.Repositorios
{
    public class Repositorio<T> : IRepositorio<T> where T : ModelBase, new()
    {
        protected readonly ContextoBanco _dbContexto;
        protected readonly DbSet<T> _dbEntidade;

        protected Repositorio(ContextoBanco dbContexto)
        {
            _dbContexto = dbContexto;
            _dbEntidade = _dbContexto.Set<T>();
        }

        public void Dispose()
        {
            _dbContexto.Dispose();
        }
    }
}
