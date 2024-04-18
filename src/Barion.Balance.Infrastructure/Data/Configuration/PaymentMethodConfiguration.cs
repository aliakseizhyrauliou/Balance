using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barion.Balance.Infrastructure.Data.Configuration;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ShowOnlyNotDeleted();

        builder.HasMany(x => x.Holds)
            .WithOne(x => x.PaymentMethod)
            .HasForeignKey(x => x.PaymentMethodId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Payments)
            .WithOne(x => x.PaymentMethod)
            .HasForeignKey(x => x.PaymentMethodId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Receipts)
            .WithOne(x => x.PaymentMethod)
            .HasForeignKey(x => x.PaymentMethodId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}