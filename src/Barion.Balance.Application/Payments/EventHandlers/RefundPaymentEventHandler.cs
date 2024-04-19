using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Events.Payments;
using MediatR;

namespace Barion.Balance.Application.Payments.EventHandlers;

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
            IsReceiptForPayment = true
        };

        notification.Payment.Receipts?.Add(receipt);
    }
}