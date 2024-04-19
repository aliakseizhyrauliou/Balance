using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Events.Holds;
using MediatR;

namespace Barion.Balance.Application.Holds.EventHandlers;

public class MakeHoldEventHandler(IReceiptRepository receiptRepository) 
    : INotificationHandler<MakeHoldEvent>
{
    public async Task Handle(MakeHoldEvent notification, 
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
            IsReceiptForHold = true,
            PaidResourceTypeId = notification.Hold.PaidResourceTypeId
        };
        
        notification.Hold.Receipts?.Add(receipt);
        
    }
}