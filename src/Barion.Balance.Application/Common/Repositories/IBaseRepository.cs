using System.Data;
using System.Linq.Expressions;
using Barion.Balance.Domain.Common;
using Microsoft.EntityFrameworkCore.Storage;

namespace Barion.Balance.Application.Common.Repositories;


public interface IBaseRepository<TEntity> where TEntity 
    : BaseEntity
{
    Task<IDbContextTransaction> BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken token = default);
    Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<TEntity> GetListAsync(CancellationToken cancellationToken = default);
}