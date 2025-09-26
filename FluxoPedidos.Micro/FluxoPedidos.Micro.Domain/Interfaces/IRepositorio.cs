using FluxoPedidos.Micro.Domain.Base;
using System.Linq.Expressions;

namespace FluxoPedidos.Micro.Domain.Interfaces
{
    public interface IRepositorio<T> : IDisposable where T : ModelBase
    {
        Task Adicionar(T entity);
        Task<T> RecuperarPorId(int id);
        Task<List<T>> RecuperarTodos();
        Task Atualizar(T entity);
        Task Remover(int id);
        Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> predicate);
        Task<bool> Existe(Expression<Func<T, bool>> predicate);
        Task<int> SaveChanges();
    }
}
