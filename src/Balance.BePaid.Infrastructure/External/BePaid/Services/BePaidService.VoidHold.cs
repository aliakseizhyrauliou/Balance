using Balance.BePaid.Domain.Entities;
using Balance.BePaid.Domain.Services.ServiceResponses;
using Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Transaction;
using Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Transaction.TransactionStatus;

namespace Balance.BePaid.Infrastructure.External.BePaid.Services;

public partial class BePaidService
{
    private async Task<ProcessVoidHoldPaymentSystemResult> ProcessVoidHoldPaymentSystemResponse(
        Hold hold,
        TransactionRoot transaction,
        CancellationToken cancellationToken = default)
    {
        return transaction.Transaction.Status switch
        {
            TransactionStatus.Successful => ProcessSuccessfulVoidHoldStatus(
                transaction, hold),
            TransactionStatus.Failed => ProcessFailedVoidHoldStatus(
                transaction, hold),
            _ => throw new NotImplementedException()
        };
    }

    private ProcessVoidHoldPaymentSystemResult ProcessSuccessfulVoidHoldStatus(TransactionRoot transaction,
        Hold voidedHold)
    {
        voidedHold.IsVoided = true;

        return new ProcessVoidHoldPaymentSystemResult
        {
            IsOk = true,
            PaymentSystemTransactionId = transaction.Transaction.Id,
            Hold = voidedHold
        };
    }

    private ProcessVoidHoldPaymentSystemResult ProcessFailedVoidHoldStatus(TransactionRoot transaction,
        Hold notVoidedHold)
    {
        return new ProcessVoidHoldPaymentSystemResult
        {
            IsOk = false,
            Hold = notVoidedHold,
            ErrorMessage = transaction.Transaction.Message,
            FriendlyErrorMessage = transaction.Transaction.Message
        };
    }
}