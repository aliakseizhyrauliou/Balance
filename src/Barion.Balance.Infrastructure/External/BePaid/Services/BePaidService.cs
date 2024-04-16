using System.Text;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Enums;
using Barion.Balance.Domain.Exceptions;
using Barion.Balance.Domain.Services;
using Barion.Balance.Domain.Services.ServiceResponses;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Response;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction.TransactionStatus;
using Barion.Balance.Infrastructure.External.BePaid.Configuration;
using Barion.Balance.Infrastructure.External.BePaid.Helpers;
using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.Services;

public class BePaidService(IPaymentSystemAuthorizationService paymentSystemAuthorizationService,
    IPaymentSystemConfigurationService configurationService,
    HttpClient httpClient) : IPaymentSystemService
{
    private const string PaymentSystemName = "BePaid";

    public async Task<int> GetWidgetId(string jsonResponse, CancellationToken cancellationToken)
    {
        var concretePaymentSystemObjectResponse = JsonConvert.DeserializeObject<TransactionRoot>(jsonResponse);

        return int.Parse(concretePaymentSystemObjectResponse.Transaction.TrackingId);
    }

    public async Task<string> GeneratePaymentSystemWidget(PaymentSystemWidgetGeneration paymentSystemWidgetGeneration,
        CancellationToken cancellationToken)
    {
        return paymentSystemWidgetGeneration.WidgetReason switch
        {
            WidgetReason.CreatePaymentMethod => await GenerateAuthorizationWidgetUrl(paymentSystemWidgetGeneration, cancellationToken),
            WidgetReason.Payment => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public async Task<ProcessCreatePaymentMethodPaymentSystemWidgetResult> ProcessCreatePaymentMethodPaymentSystemWidgetResponse(string jsonResponse,
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

    private async Task<ProcessHoldPaymentSystemResult> ProcessHoldPaymentSystemResponse(
        Hold hold,
        TransactionRoot transaction,
        CancellationToken cancellationToken = default)
    {
        return transaction.Transaction.Status switch
        {
            TransactionStatus.Successful => ProcessSuccessfulHoldStatus(
                transaction, hold),
            TransactionStatus.Failed => ProcessFailedHoldStatus(
                transaction, hold),
            _ => throw new NotImplementedException()
        };
    }

    private ProcessHoldPaymentSystemResult ProcessFailedHoldStatus(TransactionRoot transaction, Hold hold)
    {
        return new ProcessHoldPaymentSystemResult
        {
            IsOk = false,
            ErrorMessage = transaction.Transaction.Message,
            FriendlyErrorMessage = transaction.Transaction.Message
        };
    }
    private ProcessHoldPaymentSystemResult ProcessSuccessfulHoldStatus(TransactionRoot transaction, Hold hold)
    {
        hold.PaymentSystemTransactionId = transaction.Transaction.Id;

        return new ProcessHoldPaymentSystemResult
        {
            IsOk = true,
            PaymentSystemTransactionId = transaction.Transaction.Id,
            Hold = hold
        };
    }

    private async Task<ProcessCreatePaymentMethodPaymentSystemWidgetResult> ProcessFailedCreatePaymentMethodWidgetStatus(TransactionRoot concretePaymentSystemObjectResponse,
        PaymentSystemWidgetGeneration paymentSystemWidgetGeneration)
    {
        return new ProcessCreatePaymentMethodPaymentSystemWidgetResult
        {
            IsOk = false,
            PaymentMethod = null,
            PaymentSystemWidgetGeneration = paymentSystemWidgetGeneration,
            ErrorMessage = concretePaymentSystemObjectResponse.Transaction.Message,
            FriendlyErrorMessage = concretePaymentSystemObjectResponse.Transaction.Message
        };
    }

    private async Task<ProcessCreatePaymentMethodPaymentSystemWidgetResult> ProcessSuccessfulCreatePaymentMethodWidgetStatus(TransactionRoot transaction, 
        PaymentSystemWidgetGeneration paymentSystemWidgetGeneration)
    {
        var paymentMethod = new PaymentMethod
        {
            UserId = paymentSystemWidgetGeneration.UserId,
            First1 = transaction.Transaction.CreditCard.First1,
            Last4 = transaction.Transaction.CreditCard.Last4,
            PaymentSystemToken = transaction.Transaction.CreditCard.Token,
            ExpiryYear = transaction.Transaction.CreditCard.ExpYear.ToString(),
            ExpiryMonth = transaction.Transaction.CreditCard.ExpMonth.ToString(),
        };

        return new ProcessCreatePaymentMethodPaymentSystemWidgetResult
        {
            PaymentSystemWidgetGeneration = paymentSystemWidgetGeneration,
            PaymentMethod = paymentMethod,
            IsOk = true
        };
    }
    

    private async Task<string> GenerateAuthorizationWidgetUrl(PaymentSystemWidgetGeneration paymentSystemWidgetGeneration, 
        CancellationToken cancellationToken)
    {
        var configuration = await GetBePaidConfiguration();
        
        var modelForSending = BePaidModelBuilderHelper
            .BuildForAuthorizationWithWidget(configuration, paymentSystemWidgetGeneration);
        
        var httpMessage = BuildHttpRequestMessage(modelForSending, HttpMethod.Post, configuration.Urls.CheckoutUrl.Url);

        var sendResult = await SendMessageAndCast<CheckoutResponseRoot>(httpMessage, cancellationToken);

        return sendResult.CheckoutResponse.RedirectUrl;

    }

    public async Task<ProcessHoldPaymentSystemResult> MakeHold(Hold makeHold, 
        PaymentMethod paymentMethod,
        CancellationToken cancellationToken)
    {
        var configuration = await GetBePaidConfiguration();

        var modelForSending = BePaidModelBuilderHelper.BuildHoldModel(makeHold, paymentMethod.PaymentSystemToken, configuration);

        var httpMessage = BuildHttpRequestMessage(modelForSending, HttpMethod.Post, configuration.Urls.AuthorizationUrl.Url);
        
        var sendResult = await SendMessageAndCast<TransactionRoot>(httpMessage, cancellationToken);


        return await ProcessHoldPaymentSystemResponse(makeHold, sendResult, cancellationToken);
    }

    public Task<bool> CaptureHold(Hold hold, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> VoidHold(Hold hold, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    

    private async Task<BePaidConfiguration> GetBePaidConfiguration()
    {
        var configurationModel = await configurationService.GetPaymentSystemConfiguration(PaymentSystemName);

        return BePaidConfigurationDeserializationHelper.DeserializeToBePaidConfiguration(configurationModel);
    }

    private async Task<T?> SendMessageAndCast<T>(HttpRequestMessage requestMessage, 
        CancellationToken cancellationToken)
    {
        var apiResponse = await httpClient.SendAsync(requestMessage, cancellationToken);
        
        var apiContent = await apiResponse.Content.ReadAsStringAsync(cancellationToken);

        //TODO
        /*
        var test = JsonConvert.DeserializeObject<BePaidErrorRoot>(apiContent);
        */
        
        var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
        
        return apiResponseDto;
    }

    private HttpRequestMessage BuildHttpRequestMessage(object data, 
        HttpMethod httpMethod,
        string requestUrl)
    {
        var message = new HttpRequestMessage()
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