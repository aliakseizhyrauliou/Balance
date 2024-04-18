using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barion.Balance.Infrastructure.Data.Configuration;

public class HoldConfiguration: IEntityTypeConfiguration<Hold>
{
    public void Configure(EntityTypeBuilder<Hold> builder)
    {
        builder.ShowOnlyNotDeleted();

        builder.HasMany(x => x.Receipts)
            .WithOne(x => x.Hold)
            .HasForeignKey(x => x.HoldId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}