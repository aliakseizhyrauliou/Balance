using Balance.BePaid.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balance.BePaid.Infrastructure.Data.Configuration;

public class DebtorConfiguration: IEntityTypeConfiguration<Debtor>
{
    public void Configure(EntityTypeBuilder<Debtor> builder)
    {
        builder.ShowOnlyNotDeleted();
    }
}