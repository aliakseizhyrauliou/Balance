using Balance.BePaid.Application.Common.Repositories;
using Balance.BePaid.Domain.Entities;
using Balance.BePaid.Domain.Events.Payments;
using MediatR;

namespace Balance.BePaid.Application.Payments.EventHandlers;

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
            IsReceiptForPayment = true,
            PaidResourceTypeId = notification.Payment.PaidResourceTypeId
        };

        notification.Payment.Receipts?.Add(receipt);
    }
}