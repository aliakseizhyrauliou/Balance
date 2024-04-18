using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Services.ServiceResponses;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Response;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction;
using Barion.Balance.Infrastructure.External.BePaid.Helpers;
using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.Services;

public partial class BePaidService
{
    private async Task<ProcessCreatePaymentMethodPaymentSystemWidgetResult>
        ProcessFailedCreatePaymentMethodWidgetStatus(TransactionRoot concretePaymentSystemObjectResponse,
            PaymentSystemWidget paymentSystemWidget)
    {
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
            }),
        };

        return new ProcessCreatePaymentMethodPaymentSystemWidgetResult
        {
            PaymentSystemWidget = paymentSystemWidget,
            PaymentMethod = paymentMethod,
            IsOk = true
        };
    }
}