using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barion.Balance.Infrastructure.Data.Configuration;

public class PaymentSystemConfigurationConfiguration : IEntityTypeConfiguration<PaymentSystemConfiguration>
{
    public void Configure(EntityTypeBuilder<PaymentSystemConfiguration> builder)
    {
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}