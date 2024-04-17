using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Services.ServiceResponses;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction.TransactionStatus;

namespace Barion.Balance.Infrastructure.External.BePaid.Services;

public partial class BePaidService
{
    private async Task<ProcessCaptureHoldPaymentSystemResult> ProcessCaptureHoldPaymentSystemResponse(
        Hold captureHold,
        TransactionRoot transaction,
        CancellationToken cancellationToken = default)
    {
        return transaction.Transaction.Status switch
        {
            TransactionStatus.Successful => ProcessSuccessfulCaptureHoldStatus(
                transaction, captureHold),
            TransactionStatus.Failed => ProcessFailedCaptureHoldStatus(
                transaction, captureHold),
            _ => throw new NotImplementedException()
        };
    }

    private ProcessCaptureHoldPaymentSystemResult ProcessSuccessfulCaptureHoldStatus(TransactionRoot transaction,
        Hold capturedHold)
    {
        capturedHold.IsCaptured = true;

        return new ProcessCaptureHoldPaymentSystemResult
        {
            IsOk = true,
            NeedToCreateAccountRecord = true,
            Payment = new Payment
            {
                UserId = capturedHold.UserId,
                Amount = capturedHold.Amount,
                PaidResourceId = capturedHold.PaidResourceId,
                PaymentSystemFinancialTransactionId = transaction.Transaction.Id,
                OperatorId = capturedHold.OperatorId,
                IsSuccess = true,
                PaymentMethodId = capturedHold.PaymentMethodId,
                AdditionalData = capturedHold.AdditionalData,
                PaidResourceTypeId = capturedHold.PaidResourceTypeId
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