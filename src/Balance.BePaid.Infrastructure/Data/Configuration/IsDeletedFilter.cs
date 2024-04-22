using Balance.BePaid.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balance.BePaid.Infrastructure.Data.Configuration;

public static class IsDeletedFilter
{
    public static EntityTypeBuilder<TEntity> ShowOnlyNotDeleted<TEntity>(this EntityTypeBuilder<TEntity> builder) 
        where TEntity : BaseAuditableEntity
    {
        return builder.HasQueryFilter(x => !x.IsDeleted);
    }
}