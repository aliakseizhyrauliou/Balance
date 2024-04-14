using System.Data;
using System.Reflection;
using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Barion.Balance.Infrastructure.Data;

public class BalanceDbContext : DbContext, IBalanceDbContext
{
    public BalanceDbContext(DbContextOptions<BalanceDbContext> options) : base(options) { }
    

    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<AccountRecord> AccountRecords { get; set; }
    public DbSet<Hold> Holds { get; set; }
    public DbSet<PaymentSystemConfiguration> PaymentSystemConfigurations { get; set; }
    public DbSet<PaymentSystemWidgetGeneration> PaymentSystemWidgetGenerations { get; set; }

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