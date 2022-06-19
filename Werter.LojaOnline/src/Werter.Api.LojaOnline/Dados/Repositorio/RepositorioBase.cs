using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Werter.Api.LojaOnline.Negocio.Exceptions;
using Werter.Api.LojaOnline.Utils;
using Werter.LojaOnline.Compartilhado.DomainObjects;

namespace Werter.Api.LojaOnline.Dados.Repositorio
{
    public abstract class RepositorioBase<T> : DbContext, IRepositorio<T> where T : EntidadeBase, IAggregateRoot
    {
        private readonly DbSet<T> _dbSet;

        public virtual IUnitOfWork UnitOfWork => throw new NotImplementedException();

        protected RepositorioBase(LojaOnlineContext contexto) =>
            _dbSet = contexto.Set<T>();


        public void Adicionar(T entity) => _dbSet.Add(entity);


        public void Atualizar(T entity) => _dbSet.Update(entity);

        public async Task<T> BuscarPorIdAsync(Guid id, CancellationToken cancellation) =>
            await _dbSet.FirstAsync(x => x.Id == id, cancellation);

        public async Task<List<T>> ListarAsync(
            CancellationToken cancellation,
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int skip = 0, int take = 10)
        {

            var query = _dbSet.AsQueryable();

            if (expression != null) query = query.Where(expression);

            if (include != null) query = include(query);

            query = query.Skip(skip).Take(take);

            return await ExecutarComando(
                async () => await query.ToListAsync(cancellation),
                "Ocorreu um erro ao tentar listar os produtos na base de dados");
        }



        public async Task<T> PrimeiroAsync(Expression<Func<T, bool>> expression, CancellationToken cancellation)
        {

            return await _dbSet.FirstAsync(expression, cancellation);
        }


        public async Task<int> QuantidadeAsync(Expression<Func<T, bool>> expression, CancellationToken cancellation)
            => await _dbSet.CountAsync(expression, cancellation);

        public void Remover(T entity) => _dbSet.Remove(entity);
        public async Task Remover(Guid id, CancellationToken cancellationToken)
        {
            var entidade = await BuscarPorIdAsync(id, cancellationToken);
            _dbSet.Remove(entidade);
        }

        public async Task<bool> Commit(CancellationToken cancellationToken) =>
            await base.SaveChangesAsync(cancellationToken) > 0;


        private static async Task<Y> ExecutarComando<Y>(Func<Task<Y>> acao, string mensagemErro)
        {
            try
            {
                return await acao();
            }
            catch (SqlException sqlException)
            {
                throw new InfraestruturaException(mensagemErro, sqlException);
            }


        }

    }
}
