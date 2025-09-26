using FluxoPedidos.Micro.Domain.Base;
using FluxoPedidos.Micro.Domain.Interfaces;
using FluxoPedidos.Micro.Repository.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public virtual async Task<T> RecuperarPorId(int id)
        {
            return await _dbEntidade.FindAsync(id);
        }

        public virtual async Task<List<T>> RecuperarTodos()
        {
            return await _dbEntidade.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> predicate)
        {
            return await _dbEntidade.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<bool> Existe(Expression<Func<T, bool>> predicate)
        {
            return await _dbEntidade.AsNoTracking().AnyAsync(predicate);
        }

        public virtual async Task Adicionar(T entidade)
        {
            _dbEntidade.Add(entidade);
            await SaveChanges();
        }

        public virtual async Task Atualizar(T entidade)
        {
            _dbEntidade.Update(entidade);
            await SaveChanges();
        }

        public virtual async Task Remover(int id)
        {
            _dbEntidade.Remove(new T { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _dbContexto.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContexto.Dispose();
        }
    }
}
