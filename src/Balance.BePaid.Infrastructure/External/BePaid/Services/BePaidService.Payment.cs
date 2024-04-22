using Balance.BePaid.Domain.Entities;
using Balance.BePaid.Domain.Services.ServiceResponses;
using Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Transaction;
using Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Transaction.TransactionStatus;

namespace Balance.BePaid.Infrastructure.External.BePaid.Services;

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
    
    private async Task<ProcessPaymentPaymentSystemWidgetResult> ProcessSuccessfulPaymentWidgetStatus(TransactionRoot concretePaymentSystemObjectResponse,
        PaymentSystemWidget paymentSystemWidget)
    {
        var payment = new Payment
        {
            UserId = paymentSystemWidget.UserId,
            Amount = paymentSystemWidget.Amount,
            PaidResourceId = paymentSystemWidget.PaidResourceId,
            PaymentSystemTransactionId = concretePaymentSystemObjectResponse.Transaction.Id,
            OperatorId = paymentSystemWidget.OperatorId,
            PaidResourceTypeId = paymentSystemWidget.PaidResourceTypeId,
            PaymentSystemConfigurationId = paymentSystemWidget.PaymentSystemConfigurationId,
            ReceiptUrl = concretePaymentSystemObjectResponse.Transaction.ReceiptUrl,
            AdditionalData = paymentSystemWidget.AdditionalData,
            IsSuccess = true
        };

        return new ProcessPaymentPaymentSystemWidgetResult
        {
            IsOk = true,
            PaymentSystemWidget = paymentSystemWidget,
            Payment = payment
        };
    }

    private async Task<ProcessPaymentPaymentSystemWidgetResult> ProcessFailedPaymentWidgetStatus(TransactionRoot concretePaymentSystemObjectResponse, 
        PaymentSystemWidget widget)
    {
        return new ProcessPaymentPaymentSystemWidgetResult
        {
            IsOk = false,
            PaymentSystemWidget = widget,
            ErrorMessage = concretePaymentSystemObjectResponse.Transaction.Message,
            FriendlyErrorMessage = concretePaymentSystemObjectResponse.Transaction.Message
        };
    }

}