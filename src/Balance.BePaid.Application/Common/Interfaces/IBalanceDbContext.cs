using System.Data;
using Balance.BePaid.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Balance.BePaid.Application.Common.Interfaces;

public interface IBalanceDbContext
{
    DbSet<PaymentMethod> PaymentMethods { get; set; }
    DbSet<Payment> Payments { get; set; }
    DbSet<Hold> Holds { get; set; }
    DbSet<PaymentSystemConfiguration> PaymentSystemConfigurations { get; set; }
    DbSet<PaymentSystemWidget> PaymentSystemWidget { get; set; }
    DbSet<Receipt> Receipts { get; set; }
    DbSet<Debtor> Debtors { get; set; }

    public Task MigrateDatabase();
    public int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    public Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken token = default);
    public DbSet<TEntity> Set<TEntity>() where TEntity : class;
}