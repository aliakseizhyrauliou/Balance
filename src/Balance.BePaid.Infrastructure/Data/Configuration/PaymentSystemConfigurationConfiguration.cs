using Balance.BePaid.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balance.BePaid.Infrastructure.Data.Configuration;

public class PaymentSystemConfigurationConfiguration : IEntityTypeConfiguration<PaymentSystemConfiguration>
{
    public void Configure(EntityTypeBuilder<PaymentSystemConfiguration> builder)
    {
        builder.ShowOnlyNotDeleted();

        builder.HasMany(x => x.PaymentSystemWidgets)
            .WithOne(x => x.PaymentSystemConfiguration)
            .HasForeignKey(x => x.PaymentSystemConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Holds)
            .WithOne(x => x.PaymentSystemConfiguration)
            .HasForeignKey(x => x.PaymentSystemConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Payments)
            .WithOne(x => x.PaymentSystemConfiguration)
            .HasForeignKey(x => x.PaymentSystemConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Receipts)
            .WithOne(x => x.PaymentSystemConfiguration)
            .HasForeignKey(x => x.PaymentSystemConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Debtors)
            .WithOne(x => x.PaymentSystemConfiguration)
            .HasForeignKey(x => x.PaymentSystemConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}