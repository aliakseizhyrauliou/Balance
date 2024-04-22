using Balance.BePaid.Domain.Entities;
using Balance.BePaid.Domain.Events.Payments;
using MediatR;

namespace Balance.BePaid.Application.Payments.EventHandlers;

public class RefundPaymentEventHandler : INotificationHandler<RefundPaymentEvent>
{
    public async Task Handle(RefundPaymentEvent notification, CancellationToken cancellationToken)
    {
        var receipt = new Receipt
        {
            UserId = notification.Payment.UserId,
            PaidResourceId = notification.Payment.PaidResourceId,
            PaymentSystemTransactionId = notification.Payment.PaymentSystemTransactionId,
            Url = notification.RefundReceiptUrl,
            PaymentSystemConfigurationId = notification.Payment.PaymentSystemConfigurationId,
            PaymentMethodId = notification.Payment.PaymentMethodId,
            IsReceiptForPayment = true,
            PaidResourceTypeId = notification.Payment.PaidResourceTypeId
        };

        notification.Payment.Receipts?.Add(receipt);
    }
}