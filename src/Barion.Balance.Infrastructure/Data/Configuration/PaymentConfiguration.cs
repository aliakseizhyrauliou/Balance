using Barion.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barion.Balance.Infrastructure.Data.Configuration;

public class PaymentConfiguration :  IEntityTypeConfiguration<Payment>
{


    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasOne(x => x.PaidResourceType)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.PaidResourceTypeId)
            .OnDelete(DeleteBehavior.Restrict);    
    }    
}