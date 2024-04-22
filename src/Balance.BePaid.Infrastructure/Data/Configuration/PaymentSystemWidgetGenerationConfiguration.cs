using Balance.BePaid.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balance.BePaid.Infrastructure.Data.Configuration;

public class PaymentSystemWidgetGenerationConfiguration : IEntityTypeConfiguration<PaymentSystemWidget>
{
    public void Configure(EntityTypeBuilder<PaymentSystemWidget> builder)
    {
        builder.ShowOnlyNotDeleted();
    }
}