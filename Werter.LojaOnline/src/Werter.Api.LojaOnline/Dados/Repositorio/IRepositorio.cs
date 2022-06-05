using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Werter.Api.LojaOnline.Modelos;
using Werter.Api.LojaOnline.Utils;

namespace Werter.Api.LojaOnline.Dados.Repositorio
{
    public interface IRepositorio<T> where T : EntidadeBase, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        void Adicionar(T entity);
        void Remover(T entity);
        Task Remover(Guid id, CancellationToken cancellationToken);
        void Atualizar(T entity);
        

        Task<T> BuscarPorIdAsync(Guid id, CancellationToken cancellation);
        Task<T> PrimeiroAsync(Expression<Func<T, bool>> expression, CancellationToken cancellation);
        Task<int> QuantidadeAsync(Expression<Func<T, bool>> expression, CancellationToken cancellation);


        Task<List<T>> ListarAsync(
            CancellationToken cancellation,
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int skip = 0,
            int take = 10
        );
    }
}
