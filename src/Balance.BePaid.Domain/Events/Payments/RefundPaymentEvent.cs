using Balance.BePaid.Domain.Common;
using Balance.BePaid.Domain.Entities;

namespace Balance.BePaid.Domain.Events.Payments;


public class RefundPaymentEvent(Payment payment, string refundReceiptUrl) : BaseEvent
{
    public Payment Payment { get; set; } = payment;
    public string RefundReceiptUrl { get; set; } = refundReceiptUrl;
}