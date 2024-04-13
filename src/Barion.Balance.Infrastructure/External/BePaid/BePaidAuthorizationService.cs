using System.Net.Http.Headers;
using Barion.Balance.Domain.Services;

namespace Barion.Balance.Infrastructure.External.BePaid;

public class BePaidAuthorizationService : IPaymentSystemAuthorizationService
{
    public HttpRequestMessage Authorize(HttpRequestMessage requestMessage)
    {
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", "Ваш токен авторизации");

        return requestMessage;
    }
}