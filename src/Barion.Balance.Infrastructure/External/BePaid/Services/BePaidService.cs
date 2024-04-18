using System.Text;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Enums;
using Barion.Balance.Domain.Exceptions;
using Barion.Balance.Domain.Services;
using Barion.Balance.Domain.Services.ServiceResponses;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction.TransactionStatus;
using Barion.Balance.Infrastructure.External.BePaid.Configuration;
using Barion.Balance.Infrastructure.External.BePaid.Helpers;
using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.Services;

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

            return int.Parse(concretePaymentSystemObjectResponse.Transaction.TrackingId);
        }
        catch (Exception)
        {
            throw new PaymentSystemWidgetException("cannot_parse_widgetId_from_payment_system_webhook_request");
        }
    }

    public async Task<string> GeneratePaymentSystemWidget(PaymentSystemWidgetGeneration paymentSystemWidgetGeneration,
        CancellationToken cancellationToken)
    {
        return paymentSystemWidgetGeneration.WidgetReason switch
        {
            WidgetReason.CreatePaymentMethod => await GenerateAuthorizationWidgetUrl(paymentSystemWidgetGeneration,
                cancellationToken),
            WidgetReason.Payment => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public async Task<ProcessCreatePaymentMethodPaymentSystemWidgetResult>  ProcessCreatePaymentMethodPaymentSystemWidgetResponse(string jsonResponse,
            PaymentSystemWidgetGeneration paymentSystemWidgetGeneration,
            CancellationToken cancellationToken = default)
    {
        var concretePaymentSystemObjectResponse = JsonConvert.DeserializeObject<TransactionRoot>(jsonResponse);

        return concretePaymentSystemObjectResponse!.Transaction.Status switch
        {
            TransactionStatus.Successful => await ProcessSuccessfulCreatePaymentMethodWidgetStatus(
                concretePaymentSystemObjectResponse, paymentSystemWidgetGeneration),
            TransactionStatus.Failed => await ProcessFailedCreatePaymentMethodWidgetStatus(
                concretePaymentSystemObjectResponse, paymentSystemWidgetGeneration),
            TransactionStatus.Expired => await ProcessFailedCreatePaymentMethodWidgetStatus(
                concretePaymentSystemObjectResponse, paymentSystemWidgetGeneration),
            TransactionStatus.Incomplete => await ProcessFailedCreatePaymentMethodWidgetStatus(
                concretePaymentSystemObjectResponse, paymentSystemWidgetGeneration),
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
    
    private async Task<BePaidConfiguration?> CastToBePaidConfiguration(PaymentSystemConfiguration configuration)
    {
        return BePaidConfigurationDeserializationHelper.DeserializeToBePaidConfiguration(configuration);
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
