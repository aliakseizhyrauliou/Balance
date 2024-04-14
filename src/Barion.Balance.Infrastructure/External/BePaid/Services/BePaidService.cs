using System.Text;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Enums;
using Barion.Balance.Domain.Services;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Response;
using Barion.Balance.Infrastructure.External.BePaid.Configuration;
using Barion.Balance.Infrastructure.External.BePaid.Helpers;
using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.Services;

public class BePaidService(IHoldRepository holdRepository,
    IPaymentSystemAuthorizationService paymentSystemAuthorizationService,
    IPaymentSystemConfigurationService configurationService,
    HttpClient httpClient) 
    : IPaymentSystemService
{
    private const string PaymentSystemName = "BePaid";

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

    public Task<Hold> MakeHold(Hold makeHold, 
        CancellationToken cancellationToken)
    {
        
        throw new NotImplementedException();
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