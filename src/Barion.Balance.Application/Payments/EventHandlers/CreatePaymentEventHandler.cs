using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Events.Payments;
using MediatR;

namespace Barion.Balance.Application.Payments.EventHandlers;

public class CreatePaymentEventHandler(IReceiptRepository receiptRepository) 
    : INotificationHandler<CreatePaymentEvent>
{
    public async Task Handle(CreatePaymentEvent notification, CancellationToken cancellationToken)
    {
        var receipt = new Receipt
        {
            UserId = notification.Payment.UserId,
            PaidResourceId = notification.Payment.PaidResourceId,
            PaymentSystemTransactionId = notification.Payment.PaymentSystemTransactionId,
            Url = notification.Payment.ReceiptUrl,
            PaymentSystemConfigurationId = notification.Payment.PaymentSystemConfigurationId,
            PaymentMethodId = notification.Payment.PaymentMethodId,
            IsReceiptForPayment = true
        };

        notification.Payment.Receipts?.Add(receipt);
    }
}