using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barion.Balance.Infrastructure.Data.Configuration;

public class PaymentSystemWidgetGenerationConfiguration : IEntityTypeConfiguration<PaymentSystemWidgetGeneration>
{
    public void Configure(EntityTypeBuilder<PaymentSystemWidgetGeneration> builder)
    {
        builder.ShowOnlyNotDeleted();
    }
}