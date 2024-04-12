using System.Linq.Expressions;
using Barion.Balance.Domain.Common;

namespace Barion.Balance.Application.Common.Repositories;


public interface IBaseRepository<TEntity> where TEntity 
    : BaseEntity
{
    Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<TEntity> GetListAsync(CancellationToken cancellationToken = default);
}