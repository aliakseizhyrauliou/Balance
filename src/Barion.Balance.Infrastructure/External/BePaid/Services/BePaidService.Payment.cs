using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Services.ServiceResponses;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction.TransactionStatus;
using Barion.Balance.Infrastructure.External.BePaid.Configuration;

namespace Barion.Balance.Infrastructure.External.BePaid.Services;

public partial class BePaidService
{
    private async Task<ProcessPaymentPaymentSystemResult> ProcessPaymentPaymentSystemResult(
        Payment payment,
        TransactionRoot transaction,
        PaymentSystemConfiguration paymentSystemConfiguration,
        CancellationToken cancellationToken = default)
    {
        return transaction.Transaction.Status switch
        {
            TransactionStatus.Successful => ProcessSuccessfulPaymentStatus(
                transaction, paymentSystemConfiguration, payment),
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
        PaymentSystemConfiguration paymentSystemConfiguration, 
        Payment payment)
    {
        var paymentSystemTransactionId = transaction.Transaction.Id!;
        var receiptUrl = transaction.Transaction.ReceiptUrl;

        payment.IsSuccess = true;
        payment.ReceiptUrl = receiptUrl;
        payment.PaymentSystemTransactionId = paymentSystemTransactionId;
        payment.PaymentSystemConfigurationId = paymentSystemConfiguration.Id;

        return new ProcessPaymentPaymentSystemResult
        {
            IsOk = true,
            PaymentSystemTransactionId = paymentSystemTransactionId,
            Payment = payment
        };
    }

}