using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

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
    public DbSet<TEntity> Set<TEntity>() where TEntity : class;
}