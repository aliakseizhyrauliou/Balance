using Balance.BePaid.Application.Common.Repositories;
using Balance.BePaid.Domain.Entities;
using Balance.BePaid.Domain.Events.Holds;
using MediatR;

namespace Balance.BePaid.Application.Holds.EventHandlers;

public class CaptureHoldEventHandler(IReceiptRepository receiptRepository) 
    : INotificationHandler<CaptureHoldEvent>
{
    public async Task Handle(CaptureHoldEvent notification, CancellationToken cancellationToken)
    {
        var captureHoldReceipt = new Receipt
        {
            UserId = notification.Hold.UserId,
            PaidResourceId = notification.Hold.PaidResourceId,
            PaymentSystemTransactionId = notification.Hold.PaymentSystemTransactionId,
            Url = notification.Hold.ReceiptUrl,
            PaymentSystemConfigurationId = notification.Hold.PaymentSystemConfigurationId,
            PaymentMethodId = notification.Hold.PaymentMethodId,
            HoldId = notification.Hold.Id,
            IsReceiptForHold = true,
            PaidResourceTypeId = notification.Hold.PaidResourceTypeId
        };


        notification.Hold.Receipts?.Add(captureHoldReceipt);

    }
}