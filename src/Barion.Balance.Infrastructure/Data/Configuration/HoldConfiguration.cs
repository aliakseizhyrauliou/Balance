using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barion.Balance.Infrastructure.Data.Configuration;

public class HoldConfiguration: IEntityTypeConfiguration<Hold>
{
    public void Configure(EntityTypeBuilder<Hold> builder)
    {
        builder.ShowOnlyNotDeleted();

        builder.HasOne(x => x.PaymentMethod)
            .WithMany(x => x.Holds)
            .HasForeignKey(x => x.PaymentMethodId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.PaidResourceType)
            .WithMany(x => x.Holds)
            .HasForeignKey(x => x.PaidResourceTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}