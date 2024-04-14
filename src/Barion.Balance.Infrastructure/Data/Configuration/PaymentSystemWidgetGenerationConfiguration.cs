using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barion.Balance.Infrastructure.Data.Configuration;

public class PaymentSystemWidgetGenerationConfiguration : IEntityTypeConfiguration<PaymentSystemWidgetGeneration>
{
    public void Configure(EntityTypeBuilder<PaymentSystemWidgetGeneration> builder)
    {
        builder.HasQueryFilter(x => !x.IsDeleted);
        
        builder
            .HasOne(_ => _.PaymentSystemConfiguration)
            .WithMany(_ => _.PaymentSystemWidgetGenerations)
            .HasForeignKey(x => x.PaymentSystemConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}