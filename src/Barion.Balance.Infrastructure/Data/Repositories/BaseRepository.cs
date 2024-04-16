using System.Data;
using System.Linq.Expressions;
using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Common;
using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Barion.Balance.Infrastructure.Data.Repositories;

public class BaseRepository<TEntity>
    : IBaseRepository<TEntity> where TEntity : BaseEntity 
{
    private readonly IBalanceDbContext _context;
    protected readonly DbSet<TEntity> dbSet;

    protected BaseRepository(IBalanceDbContext context)
    {
        _context = context;
        dbSet = _context.Set<TEntity>();
    }

    public Task<IDbContextTransaction> BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, 
        CancellationToken token = default)
    {
        return _context.BeginTransactionAsync(isolationLevel, token);
    }

    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken)
    {
        return await dbSet.AnyAsync(predicate, cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, 
        CancellationToken cancellationToken = default)
    {
        dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, 
        CancellationToken cancellationToken = default)
    {
        dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(int id, 
        CancellationToken cancellationToken = default)
    {
        return await dbSet.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return await dbSet.ToListAsync(cancellationToken);
    }
}