using System.Data;
using System.Reflection;
using Balance.BePaid.Application.Common.Interfaces;
using Balance.BePaid.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Balance.BePaid.Infrastructure.Data;

public class BalanceDbContext(DbContextOptions<BalanceDbContext> options)
    : DbContext(options), IBalanceDbContext
{
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Hold> Holds { get; set; }
    public DbSet<PaymentSystemConfiguration> PaymentSystemConfigurations { get; set; }
    public DbSet<PaymentSystemWidget> PaymentSystemWidget { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<Debtor> Debtors { get; set; }

    public async Task MigrateDatabase()
    {
        await Database.MigrateAsync();
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken token = default)
    {
        return Database.BeginTransactionAsync(isolationLevel, token);
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
}