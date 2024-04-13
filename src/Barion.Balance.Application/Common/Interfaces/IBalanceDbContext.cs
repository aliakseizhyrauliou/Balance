using System.Data;
using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Barion.Balance.Application.Common.Interfaces;

public interface IBalanceDbContext
{
    DbSet<PaymentMethod> PaymentMethods { get; set; }
    DbSet<AccountRecord> AccountRecords { get; set; }
    DbSet<Hold> Holds { get; set; }
    DbSet<PaymentSystemConfiguration> PaymentSystemConfigurations { get; set; }

    public Task MigrateDatabase();
    public int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    public Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken token = default);
    public DbSet<TEntity> Set<TEntity>() where TEntity : class;
}