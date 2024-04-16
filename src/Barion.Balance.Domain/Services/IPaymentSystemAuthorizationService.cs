namespace Barion.Balance.Domain.Services;

public interface IPaymentSystemAuthorizationService
{
    
    /// <summary>
    /// Авторизует запрос перед отправкой платежной системе
    /// </summary>
    /// <param name="requestMessage"></param>
    /// <returns></returns>
    public HttpRequestMessage Authorize(HttpRequestMessage requestMessage);
}