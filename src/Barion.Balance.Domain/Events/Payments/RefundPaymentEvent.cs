using Barion.Balance.Domain.Common;
using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Domain.Events.Payments;


public class RefundPaymentEvent(Payment payment, string refundReceiptUrl) : BaseEvent
{
    public Payment Payment { get; set; } = payment;
    public string RefundReceiptUrl { get; set; } = refundReceiptUrl;
}