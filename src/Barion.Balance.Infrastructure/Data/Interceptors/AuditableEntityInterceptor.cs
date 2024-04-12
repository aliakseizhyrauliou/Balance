using Barion.Balance.Application.Common.Interfaces;
using Barion.Balance.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Barion.Balance.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor(TimeProvider dateTime, 
    IUser _user)
    : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, 
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entry.State is not (EntityState.Added or EntityState.Modified or EntityState.Deleted) &&
                !entry.HasChangedOwnedEntities()) continue;
            
            var utcNow = dateTime.GetUtcNow();
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = _user.Id;
                entry.Entity.Created = utcNow;
            }
            else if (entry.State == EntityState.Deleted)
            {
                entry.Entity.LastModifiedBy = _user.Id;
                entry.Entity.LastModified = utcNow;
                entry.Entity.IsDeleted = true;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedBy = _user.Id;
                entry.Entity.LastModified = utcNow;
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r => 
            r.TargetEntry != null && 
            r.TargetEntry.Metadata.IsOwned() && 
            r.TargetEntry.State is EntityState.Added or EntityState.Modified);
}