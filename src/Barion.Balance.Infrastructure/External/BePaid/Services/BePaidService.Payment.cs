using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Services.ServiceResponses;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction.TransactionStatus;

namespace Barion.Balance.Infrastructure.External.BePaid.Services;

public partial class BePaidService
{
    private async Task<ProcessPaymentPaymentSystemResult> ProcessPaymentPaymentSystemResult(
        Payment payment,
        TransactionRoot transaction,
        CancellationToken cancellationToken = default)
    {
        return transaction.Transaction.Status switch
        {
            TransactionStatus.Successful => ProcessSuccessfulPaymentStatus(
                transaction, payment),
            TransactionStatus.Failed => ProcessFailedPaymentStatus(
                transaction, payment),
            _ => throw new NotImplementedException()
        };
    }

    private ProcessPaymentPaymentSystemResult ProcessFailedPaymentStatus(TransactionRoot transaction, 
        Payment payment)
    {
        return new ProcessPaymentPaymentSystemResult()
        {
            IsOk = false,
            ErrorMessage = transaction.Transaction.Message,
            FriendlyErrorMessage = transaction.Transaction.Message
        };
    }

    private ProcessPaymentPaymentSystemResult ProcessSuccessfulPaymentStatus(TransactionRoot transaction, 
        Payment payment)
    {
        var paymentSystemTransactionId = transaction.Transaction.Id!;
        payment.PaymentSystemFinancialTransactionId = paymentSystemTransactionId;
        payment.IsSuccess = true;

        return new ProcessPaymentPaymentSystemResult
        {
            IsOk = true,
            PaymentSystemTransactionId = paymentSystemTransactionId,
            AccountRecord = payment
        };
    }

}