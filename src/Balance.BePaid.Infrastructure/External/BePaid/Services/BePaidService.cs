using System.Text;
using Balance.BePaid.Domain.Entities;
using Balance.BePaid.Domain.Enums;
using Balance.BePaid.Domain.Exceptions;
using Balance.BePaid.Domain.Services;
using Balance.BePaid.Domain.Services.ServiceResponses;
using Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Transaction;
using Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Transaction.TransactionStatus;
using Balance.BePaid.Infrastructure.External.BePaid.Configuration;
using Balance.BePaid.Infrastructure.External.BePaid.Helpers;
using Newtonsoft.Json;

namespace Balance.BePaid.Infrastructure.External.BePaid.Services;

public partial class BePaidService(
    IPaymentSystemAuthorizationService paymentSystemAuthorizationService,
    IPaymentSystemConfigurationService configurationService,
    HttpClient httpClient) : IPaymentSystemService
{
    private const string PaymentSystemName = "BePaid";
    

    public async Task<int> GetWidgetId(string jsonResponse, CancellationToken cancellationToken)
    {
        try
        {
            var concretePaymentSystemObjectResponse = JsonConvert.DeserializeObject<TransactionRoot>(jsonResponse);

            if (concretePaymentSystemObjectResponse is null)
            {
                throw new PaymentSystemWidgetException("cannot_parse_widgetId_from_payment_system_webhook_request");
            }

            return int.Parse(concretePaymentSystemObjectResponse.Transaction.TrackingId);
        }
        catch (Exception)
        {
            throw new PaymentSystemWidgetException("cannot_parse_widgetId_from_payment_system_webhook_request");
        }
    }

    public async Task<string> GeneratePaymentSystemWidget(PaymentSystemWidget paymentSystemWidget,
        PaymentSystemConfiguration paymentSystemConfiguration,
        CancellationToken cancellationToken)
    {
        return paymentSystemWidget.WidgetReason switch
        {
            WidgetReason.CreatePaymentMethod => await GenerateWidgetForCreatePaymentMethod(paymentSystemWidget,
                paymentSystemConfiguration, cancellationToken),
            WidgetReason.Payment => await GenerateWidgetForPayment(paymentSystemWidget,
                paymentSystemConfiguration,
                cancellationToken),
            WidgetReason.Hold => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public async Task<ProcessCreatePaymentMethodPaymentSystemWidgetResult>  ProcessCreatePaymentMethodPaymentSystemWidgetResponse(string jsonResponse,
            PaymentSystemWidget paymentSystemWidget,
            CancellationToken cancellationToken = default)
    {
        var concretePaymentSystemObjectResponse = JsonConvert.DeserializeObject<TransactionRoot>(jsonResponse);

        return concretePaymentSystemObjectResponse!.Transaction.Status switch
        {
            TransactionStatus.Successful => await ProcessSuccessfulCreatePaymentMethodWidgetStatus(
                concretePaymentSystemObjectResponse, paymentSystemWidget),
            TransactionStatus.Failed => await ProcessFailedCreatePaymentMethodWidgetStatus(
                concretePaymentSystemObjectResponse, paymentSystemWidget),
            TransactionStatus.Expired => await ProcessFailedCreatePaymentMethodWidgetStatus(
                concretePaymentSystemObjectResponse, paymentSystemWidget),
            TransactionStatus.Incomplete => await ProcessFailedCreatePaymentMethodWidgetStatus(
                concretePaymentSystemObjectResponse, paymentSystemWidget),
            _ => throw new NotImplementedException()
        };
    }

    public async Task<ProcessPaymentPaymentSystemWidgetResult> ProcessPaymentSystemWidgetResponse(string jsonResponse,
        PaymentSystemWidget widget,
        CancellationToken cancellationToken = default)
    {
        var concretePaymentSystemObjectResponse = JsonConvert.DeserializeObject<TransactionRoot>(jsonResponse);

        return concretePaymentSystemObjectResponse!.Transaction.Status switch
        {
            TransactionStatus.Successful => await ProcessSuccessfulPaymentWidgetStatus(
                concretePaymentSystemObjectResponse, widget),
            TransactionStatus.Failed => await ProcessFailedPaymentWidgetStatus(
                concretePaymentSystemObjectResponse, widget),
            TransactionStatus.Expired => await ProcessFailedPaymentWidgetStatus(
                concretePaymentSystemObjectResponse, widget),
            TransactionStatus.Incomplete => await ProcessFailedPaymentWidgetStatus(
                concretePaymentSystemObjectResponse, widget),
            _ => throw new NotImplementedException()
        };
    }


    public async Task<ProcessHoldPaymentSystemResult> Hold(Hold makeHold,
        PaymentMethod paymentMethod,
        PaymentSystemConfiguration paymentSystemConfiguration,
        CancellationToken cancellationToken)
    {
        var bePaidConfiguration = await CastToBePaidConfiguration(paymentSystemConfiguration);
        
        var modelForSending =
            BePaidModelBuilderHelper.BuildHoldModel(makeHold, paymentMethod.PaymentSystemToken, bePaidConfiguration);

        var httpMessage =
            BuildHttpRequestMessage(modelForSending, HttpMethod.Post, bePaidConfiguration.Urls.AuthorizationUrl.Url);

        var sendResult = await SendMessageAndCast<TransactionRoot>(httpMessage, cancellationToken);

        return await ProcessHoldPaymentSystemResponse(makeHold, sendResult, cancellationToken);
    }


    public async Task<ProcessVoidHoldPaymentSystemResult> VoidHold(Hold voidHold, 
        PaymentSystemConfiguration paymentSystemConfiguration,
        CancellationToken cancellationToken)
    {
        var bePaidConfiguration = await CastToBePaidConfiguration(paymentSystemConfiguration);

        var modelForSending =
            BePaidModelBuilderHelper.BuildParentIdModel(voidHold.PaymentSystemTransactionId, 
                BePaidAmountConverterHelper.ConvertToBePaidFormat(voidHold.Amount));

        var httpMessage = BuildHttpRequestMessage(modelForSending, HttpMethod.Post, bePaidConfiguration.Urls.VoidHold.Url);

        var sendResult = await SendMessageAndCast<TransactionRoot>(httpMessage, cancellationToken);

        return await ProcessVoidHoldPaymentSystemResponse(voidHold, sendResult, cancellationToken);
    }

    public async Task<ProcessPaymentPaymentSystemResult> Payment(Payment payment,
        PaymentMethod paymentMethod,
        PaymentSystemConfiguration paymentSystemConfiguration,
        CancellationToken cancellationToken)
    {
        var bePaidConfiguration = await CastToBePaidConfiguration(paymentSystemConfiguration);
        
        var modelForSending =
            BePaidModelBuilderHelper.BuildPaymentModel(BePaidAmountConverterHelper.ConvertToBePaidFormat(payment.Amount),
                paymentMethod.PaymentSystemToken, bePaidConfiguration);

        var httpMessage =
            BuildHttpRequestMessage(modelForSending, HttpMethod.Post, bePaidConfiguration.Urls.PaymentUrl.Url);

        var sendResult = await SendMessageAndCast<TransactionRoot>(httpMessage, cancellationToken);

        return await ProcessPaymentPaymentSystemResult(payment, sendResult, paymentSystemConfiguration, cancellationToken);
    }

    public async Task<ProcessRefundPaymentSystemResult> Refund(Payment payment, 
        PaymentSystemConfiguration paymentSystemConfiguration,
        CancellationToken cancellationToken)
    {
        const string refundReason = "Возврат сердств";
        
        var bePaidConfiguration = await CastToBePaidConfiguration(paymentSystemConfiguration);

        var modelForSending =
            BePaidModelBuilderHelper.BuildParentIdModel(payment.PaymentSystemTransactionId, 
                BePaidAmountConverterHelper.ConvertToBePaidFormat(payment.Amount), refundReason);

        var httpMessage = BuildHttpRequestMessage(modelForSending, HttpMethod.Post, bePaidConfiguration.Urls.RefundUrl.Url);

        var sendResult = await SendMessageAndCast<TransactionRoot>(httpMessage, cancellationToken);

        return await ProcessRefundPaymentSystemResponse(payment, sendResult, cancellationToken);    
    }

    public async Task<ProcessCaptureHoldPaymentSystemResult> CaptureHold(Hold captureHold,
        PaymentSystemConfiguration paymentSystemConfiguration,
        CancellationToken cancellationToken)
    {
        var bePaidConfiguration = await CastToBePaidConfiguration(paymentSystemConfiguration);

        var modelForSending = BePaidModelBuilderHelper.BuildParentIdModel(captureHold.PaymentSystemTransactionId, 
            BePaidAmountConverterHelper.ConvertToBePaidFormat(captureHold.Amount));

        var httpMessage = BuildHttpRequestMessage(modelForSending, HttpMethod.Post, bePaidConfiguration.Urls.CaptureHoldUrl.Url);

        var sendResult = await SendMessageAndCast<TransactionRoot>(httpMessage, cancellationToken);

        return await ProcessCaptureHoldPaymentSystemResponse(captureHold, sendResult, paymentSystemConfiguration, cancellationToken);
    }


    private async Task<BePaidConfiguration> GetBePaidConfiguration()
    {
        var configurationModel = await configurationService.GetPaymentSystemConfiguration(PaymentSystemName);

        return BePaidConfigurationDeserializationHelper.DeserializeToBePaidConfiguration(configurationModel);
    }
    
    private async Task<BePaidConfiguration> CastToBePaidConfiguration(PaymentSystemConfiguration configuration)
    {
        var bePaidConfig = BePaidConfigurationDeserializationHelper.DeserializeToBePaidConfiguration(configuration);

        if (bePaidConfig is null)
        {
            throw new Exception("cannot_get_payment_system_config");
        }

        return bePaidConfig;
    }


    private async Task<T?> SendMessageAndCast<T>(HttpRequestMessage requestMessage,
        CancellationToken cancellationToken)
    {
        var apiResponse = await httpClient.SendAsync(requestMessage, cancellationToken);

        var apiContent = await apiResponse.Content.ReadAsStringAsync(cancellationToken);

        if (!apiResponse.IsSuccessStatusCode)
        {
            throw new PaymentSystemException(apiContent);
        }

        var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);

        return apiResponseDto;
    }

    private HttpRequestMessage BuildHttpRequestMessage(object data,
        HttpMethod httpMethod,
        string requestUrl)
    {
        var message = new HttpRequestMessage
        {
            RequestUri = new Uri(requestUrl),
            Method = httpMethod
        };

        paymentSystemAuthorizationService.Authorize(message);

        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        message.Content = content;

        return message;
    }
}
