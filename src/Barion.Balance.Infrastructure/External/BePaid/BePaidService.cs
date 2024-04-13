using System.Text;
using Barion.Balance.Application.Common.Repositories;
using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Services;
using Barion.Balance.Domain.Services.Models;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels;
using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid;

public class BePaidService(IHoldRepository holdRepository,
    IPaymentSystemAuthorizationService paymentSystemAuthorizationService,
    BePaidConfigurationService configurationService,
    HttpClient httpClient) 
    : IPaymentSystemService
{
    private const string PaymentSystemName = "BePaid";
    public Task<Hold> MakeHold(MakeHold makeHold, 
        PaymentSystemConfiguration paymentSystemConfiguration,
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
    
    private async Task<CheckouRootDto> GenerateAuthorizationWidgetUrl(MakeHold hold)
    {
        var configuration = await GetBePaidConfiguration();
        
        var modelForSending = BePaidModelBuilder.BuildForAuthorizationWithWidget(configuration, hold.PaymentMethodId);

        var httpMessage = BuildHttpRequestMessage(modelForSending, HttpMethod.Post, configuration.Urls.CheckoutUrl.Url);

        var apiResponse = await httpClient.SendAsync(httpMessage);
        
        var apiContent = await apiResponse.Content.ReadAsStringAsync();

        var apiResponseDto = JsonConvert.DeserializeObject<CheckouRootDto>(apiContent);
        
        return apiResponseDto;

    }

    private async Task<BePaidConfiguration> GetBePaidConfiguration()
    {
        var configurationModel = await configurationService.GetPaymentSystemConfiguration(PaymentSystemName);

        return BePaidConfigurationDeserializationHelper.DeserializeToBePaidConfiguration(configurationModel);
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