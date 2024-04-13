namespace Barion.Balance.Domain.Services;

public interface IPaymentSystemAuthorizationService
{
    public HttpRequestMessage Authorize(HttpRequestMessage requestMessage);
}