using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Services.ServiceResponses;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction.TransactionStatus;

namespace Barion.Balance.Infrastructure.External.BePaid.Services;

public partial class BePaidService
{

    private async Task<ProcessHoldPaymentSystemResult> ProcessHoldPaymentSystemResponse(
        Hold hold,
        TransactionRoot transaction,
        CancellationToken cancellationToken = default)
    {
        return transaction.Transaction.Status switch
        {
            TransactionStatus.Successful => ProcessSuccessfulHoldStatus(
                transaction, hold),
            TransactionStatus.Failed => ProcessFailedHoldStatus(
                transaction, hold),
            _ => throw new NotImplementedException()
        };
    }
    

    private ProcessHoldPaymentSystemResult ProcessFailedHoldStatus(TransactionRoot transaction, Hold hold)
    {
        return new ProcessHoldPaymentSystemResult
        {
            IsOk = false,
            ErrorMessage = transaction.Transaction.Message,
            FriendlyErrorMessage = transaction.Transaction.Message
        };
    }

    private ProcessHoldPaymentSystemResult ProcessSuccessfulHoldStatus(TransactionRoot transaction, Hold hold)
    {
        var receiptUrl = transaction.Transaction.ReceiptUrl;
        
        hold.PaymentSystemTransactionId = transaction.Transaction.Id;
        hold.ReceiptUrl = receiptUrl;
        
        return new ProcessHoldPaymentSystemResult
        {
            IsOk = true,
            PaymentSystemTransactionId = transaction.Transaction.Id,
            Hold = hold
        };
    }
}