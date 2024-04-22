using Balance.BePaid.Domain.Entities;
using Balance.BePaid.Domain.Services.ServiceResponses;
using Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Transaction;
using Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Transaction.TransactionStatus;

namespace Balance.BePaid.Infrastructure.External.BePaid.Services;

public partial class BePaidService
{
    private async Task<ProcessRefundPaymentSystemResult> ProcessRefundPaymentSystemResponse(Payment payment, 
        TransactionRoot? transaction,
        CancellationToken cancellationToken)
    {
        return transaction.Transaction.Status switch
        {
            TransactionStatus.Successful => ProcessSuccessfulRefundStatus(
                transaction, payment),
            TransactionStatus.Failed => ProcessFailedRefundStatus(
                transaction, payment),
            _ => throw new NotImplementedException()
        };    
    }

    private ProcessRefundPaymentSystemResult ProcessFailedRefundStatus(TransactionRoot transaction,
        Payment payment)
    {
        return new ProcessRefundPaymentSystemResult
        {
            IsOk = false,
            Payment = payment,
            ErrorMessage = transaction.Transaction.Message,
            FriendlyErrorMessage = transaction.Transaction.Message
        };
    }

    private ProcessRefundPaymentSystemResult ProcessSuccessfulRefundStatus(TransactionRoot transaction, 
        Payment payment)
    {
        payment.IsRefund = true;

        return new ProcessRefundPaymentSystemResult
        {
            IsOk = true,
            Payment = payment,
            RefundPaymentUrl = transaction.Transaction.ReceiptUrl
        };
    }
}