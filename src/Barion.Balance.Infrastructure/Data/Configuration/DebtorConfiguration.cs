using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barion.Balance.Infrastructure.Data.Configuration;

public class DebtorConfiguration: IEntityTypeConfiguration<Debtor>
{
    public void Configure(EntityTypeBuilder<Debtor> builder)
    {
        builder.ShowOnlyNotDeleted();
    }
}