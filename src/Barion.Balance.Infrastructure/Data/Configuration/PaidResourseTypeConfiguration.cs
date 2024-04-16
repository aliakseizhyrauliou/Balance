using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barion.Balance.Infrastructure.Data.Configuration;

public class PaidResourseTypeConfiguration: IEntityTypeConfiguration<PaidResourceType>
{
    public void Configure(EntityTypeBuilder<PaidResourceType> builder)
    {
        builder.ShowOnlyNotDeleted();
    }
}