using FluxoPedidos.Micro.Domain.Interfaces;
using FluxoPedidos.Micro.Domain.Models.Pedidos;
using FluxoPedidos.Micro.Repository.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace FluxoPedidos.Micro.Repository.Repositorios.Pedidos
{
    public class PedidoRepositorio : Repositorio<Pedido>, IPedidoRepositorio
    {
        public PedidoRepositorio(ContextoBanco dbContexto) : base(dbContexto)
        {
        }

        public async Task<List<Pedido>> RecuperarTodos()
        {
            return await _dbEntidade.AsNoTracking()
                .Include(p => p.Cliente)
                .Include(p => p.Itens)
                .ToListAsync();
        }

        public async Task<Pedido> RecuperarPorId(int id)
        {
            return await _dbEntidade.AsNoTracking()
                .Include(p => p.Cliente)
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pedido>> Buscar(Expression<Func<Pedido, bool>> predicate)
        {
            return await _dbEntidade.AsNoTracking()
                .Include(p => p.Cliente)
                .Include(p => p.Itens)
                .Where(predicate).ToListAsync();
        }
    }
}
