using Balance.BePaid.Domain.Entities;
using Balance.BePaid.Domain.Services.ServiceResponses;
using Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Transaction;
using Newtonsoft.Json;

namespace Balance.BePaid.Infrastructure.External.BePaid.Services;

public partial class BePaidService
{
    private async Task<ProcessCreatePaymentMethodPaymentSystemWidgetResult>
        ProcessFailedCreatePaymentMethodWidgetStatus(TransactionRoot concretePaymentSystemObjectResponse,
            PaymentSystemWidget paymentSystemWidget)
    {
        paymentSystemWidget.PaymentSystemMessage =
            concretePaymentSystemObjectResponse.Transaction.Message ?? "Error from payment system";
        
        return new ProcessCreatePaymentMethodPaymentSystemWidgetResult
        {
            IsOk = false,
            PaymentMethod = null,
            PaymentSystemWidget = paymentSystemWidget,
            ErrorMessage = concretePaymentSystemObjectResponse.Transaction.Message,
            FriendlyErrorMessage = concretePaymentSystemObjectResponse.Transaction.Message
        };
    }

    private async Task<ProcessCreatePaymentMethodPaymentSystemWidgetResult>
        ProcessSuccessfulCreatePaymentMethodWidgetStatus(TransactionRoot transaction,
            PaymentSystemWidget paymentSystemWidget)
    {
        paymentSystemWidget.PaymentSystemMessage = transaction.Transaction.Message;
        
        var paymentMethod = new PaymentMethod
        {
            UserId = paymentSystemWidget.UserId,
            First1 = transaction.Transaction.CreditCard.First1,
            Last4 = transaction.Transaction.CreditCard.Last4,
            PaymentSystemToken = transaction.Transaction.CreditCard.Token,
            ExpiryYear = transaction.Transaction.CreditCard.ExpYear,
            ExpiryMonth = transaction.Transaction.CreditCard.ExpMonth,
            CardNumberData = JsonConvert.SerializeObject(new
            {
                FirstOne = transaction.Transaction.CreditCard.First1,
                LastFour = transaction.Transaction.CreditCard.Last4
            })
        };

        return new ProcessCreatePaymentMethodPaymentSystemWidgetResult
        {
            PaymentSystemWidget = paymentSystemWidget,
            PaymentMethod = paymentMethod,
            IsOk = true
        };
    }   
}