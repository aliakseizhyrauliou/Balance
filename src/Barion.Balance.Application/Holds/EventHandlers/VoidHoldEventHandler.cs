using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Events.Holds;
using MediatR;

namespace Barion.Balance.Application.Holds.EventHandlers;


public class VoidHoldEventHandler(IReceiptRepository receiptRepository) 
    : INotificationHandler<VoidHoldEvent>
{
    public async Task Handle(VoidHoldEvent notification, 
        CancellationToken cancellationToken)
    {
        var receipt = new Receipt
        {
            UserId = notification.Hold.UserId,
            PaidResourceId = notification.Hold.PaidResourceId,
            PaymentSystemTransactionId = notification.Hold.PaymentSystemTransactionId,
            Url = notification.Hold.ReceiptUrl,
            PaymentSystemConfigurationId = notification.Hold.PaymentSystemConfigurationId,
            PaymentMethodId = notification.Hold.PaymentMethodId,
            HoldId = notification.Hold.Id,
            IsReceiptForHold = true
        };


        notification.Hold.Receipts?.Add(receipt);
    }
}