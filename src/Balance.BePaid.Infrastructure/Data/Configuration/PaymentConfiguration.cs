using Balance.BePaid.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balance.BePaid.Infrastructure.Data.Configuration;

public class PaymentConfiguration :  IEntityTypeConfiguration<Payment>
{
    
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ShowOnlyNotDeleted();
        //Чек
        builder.HasMany(x => x.Receipts)
            .WithOne(x => x.Payment)
            .HasForeignKey(x => x.PaymentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.PaymentSystemWidgets)
            .WithOne(x => x.Payment)
            .HasForeignKey<PaymentSystemWidget>(x => x.PaymentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CaptureDebtor)
            .WithOne(x => x.NewPayment)
            .HasForeignKey<Debtor>(x => x.NewPaymentId)
            .OnDelete(DeleteBehavior.Cascade);
    }    
}