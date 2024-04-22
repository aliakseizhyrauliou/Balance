using Balance.BePaid.Domain.Entities;
using Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Checkout.Response;
using Balance.BePaid.Infrastructure.External.BePaid.Helpers;

namespace Balance.BePaid.Infrastructure.External.BePaid.Services;

public partial class BePaidService
{
    private async Task<string> GenerateWidgetForCreatePaymentMethod(
        PaymentSystemWidget paymentSystemWidget,
        PaymentSystemConfiguration paymentSystemConfiguration,
        CancellationToken cancellationToken)
    {
        var configuration = await CastToBePaidConfiguration(paymentSystemConfiguration);

        var modelForSending = BePaidModelBuilderHelper
            .BuildWidgetForCreatePaymentMethod(configuration!, paymentSystemWidget);

        var httpMessage = BuildHttpRequestMessage(modelForSending, HttpMethod.Post, configuration.Urls.CheckoutUrl.Url);

        var sendResult = await SendMessageAndCast<CheckoutResponseRoot>(httpMessage, cancellationToken);

        return sendResult.CheckoutResponse.RedirectUrl;
    }

    private async Task<string> GenerateWidgetForPayment(PaymentSystemWidget paymentSystemWidget, 
        PaymentSystemConfiguration paymentSystemConfiguration,
        CancellationToken cancellationToken)
    {
        var configuration = await CastToBePaidConfiguration(paymentSystemConfiguration);

        var modelForSending = BePaidModelBuilderHelper
            .BuildWidgetForPayment(configuration!, 
                paymentSystemWidget,
                BePaidAmountConverterHelper.ConvertToBePaidFormat(paymentSystemWidget.Amount));

        var httpMessage = BuildHttpRequestMessage(modelForSending, HttpMethod.Post, configuration.Urls.CheckoutUrl.Url);

        var sendResult = await SendMessageAndCast<CheckoutResponseRoot>(httpMessage, cancellationToken);

        return sendResult.CheckoutResponse.RedirectUrl;
    }
    
}