using Balance.BePaid.Domain.Entities;
using Balance.BePaid.Domain.Services.ServiceResponses;
using Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Transaction;
using Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Transaction.TransactionStatus;

namespace Balance.BePaid.Infrastructure.External.BePaid.Services;

public partial class BePaidService
{
    private async Task<ProcessCaptureHoldPaymentSystemResult> ProcessCaptureHoldPaymentSystemResponse(
        Hold captureHold,
        TransactionRoot transaction,
        PaymentSystemConfiguration paymentSystemConfiguration,
        CancellationToken cancellationToken = default)
    {
        return transaction.Transaction.Status switch
        {
            TransactionStatus.Successful => ProcessSuccessfulCaptureHoldStatus(
                transaction, captureHold, paymentSystemConfiguration),
            TransactionStatus.Failed => ProcessFailedCaptureHoldStatus(
                transaction, captureHold),
            _ => throw new NotImplementedException()
        };
    }

    private ProcessCaptureHoldPaymentSystemResult ProcessSuccessfulCaptureHoldStatus(TransactionRoot transaction,
        Hold capturedHold,
        PaymentSystemConfiguration paymentSystemConfiguration)
    {
        var receiptUrl = transaction.Transaction.ReceiptUrl;

        capturedHold.IsCaptured = true;

        return new ProcessCaptureHoldPaymentSystemResult
        {
            IsOk = true,
            NeedToCreatePaymentRecord = true,
            Payment = new Payment
            {
                UserId = capturedHold.UserId,
                Amount = capturedHold.Amount,
                PaidResourceId = capturedHold.PaidResourceId,
                PaymentSystemTransactionId = transaction.Transaction.Id,
                OperatorId = capturedHold.OperatorId,
                IsSuccess = true,
                PaymentMethodId = capturedHold.PaymentMethodId,
                AdditionalData = capturedHold.AdditionalData,
                PaidResourceTypeId = capturedHold.PaidResourceTypeId,
                ReceiptUrl = receiptUrl,
                PaymentSystemConfigurationId = paymentSystemConfiguration.Id,
                CapturedHoldId = capturedHold.Id
            },
            PaymentSystemTransactionId = transaction.Transaction.Id,
            Hold = capturedHold
        };
    }

    private ProcessCaptureHoldPaymentSystemResult ProcessFailedCaptureHoldStatus(TransactionRoot transaction,
        Hold notCapturedHold)
    {
        return new ProcessCaptureHoldPaymentSystemResult
        {
            IsOk = false,
            ErrorMessage = transaction.Transaction.Message,
            FriendlyErrorMessage = transaction.Transaction.Message,
            Hold = notCapturedHold
        };
    }
}