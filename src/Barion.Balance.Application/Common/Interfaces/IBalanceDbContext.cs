using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Barion.Balance.Application.Common.Interfaces;

public interface IBalanceDbContext
{
    DbSet<PaymentMethod> PaymentMethods { get; set; }
    DbSet<AccountRecord> AccountRecords { get; set; }
    DbSet<Hold> Holds { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    public DbSet<TEntity> Set<TEntity>() where TEntity : class;
}