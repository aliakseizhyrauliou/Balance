using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barion.Balance.Infrastructure.Data.Configuration;

public class PaidResourceTypeConfiguration: IEntityTypeConfiguration<PaidResourceType>
{
    public void Configure(EntityTypeBuilder<PaidResourceType> builder)
    {
        builder.ShowOnlyNotDeleted();

        builder.HasMany(x => x.Payments)
            .WithOne(x => x.PaidResourceType)
            .HasForeignKey(x => x.PaidResourceTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(x => x.Holds)
            .WithOne(x => x.PaidResourceType)
            .HasForeignKey(x => x.PaidResourceTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(x => x.PaymentSystemWidgetGenerations)
            .WithOne(x => x.PaidResourceType)
            .HasForeignKey(x => x.PaidResourceTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}