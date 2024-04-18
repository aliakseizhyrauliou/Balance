using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barion.Balance.Infrastructure.Data.Configuration;

public class PaymentSystemConfigurationConfiguration : IEntityTypeConfiguration<PaymentSystemConfiguration>
{
    public void Configure(EntityTypeBuilder<PaymentSystemConfiguration> builder)
    {
        builder.ShowOnlyNotDeleted();

        builder.HasMany(x => x.PaymentSystemWidgetGenerations)
            .WithOne(x => x.PaymentSystemConfiguration)
            .HasForeignKey(x => x.PaymentSystemConfigurationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Holds)
            .WithOne(x => x.PaymentSystemConfiguration)
            .HasForeignKey(x => x.PaymentSystemConfigurationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Payments)
            .WithOne(x => x.PaymentSystemConfiguration)
            .HasForeignKey(x => x.PaymentSystemConfigurationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Receipts)
            .WithOne(x => x.PaymentSystemConfiguration)
            .HasForeignKey(x => x.PaymentSystemConfigurationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}