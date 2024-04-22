using System.Net.Http.Headers;
using Balance.BePaid.Domain.Services;
using Microsoft.Extensions.Configuration;

namespace Balance.BePaid.Infrastructure.External.BePaid.Services;

public class BePaidAuthorizationService(IConfiguration configuration) 
    : IPaymentSystemAuthorizationService
{
    public HttpRequestMessage Authorize(HttpRequestMessage requestMessage)
    {
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", configuration["BEPAID_TOKEN"]);

        return requestMessage;
    }

    public bool ValidateReceivedWebHookRequest(string headerValue)
    {
        return headerValue == configuration["BEPAID_TOKEN"];
    }
}