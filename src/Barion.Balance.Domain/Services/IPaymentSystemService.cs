using Barion.Balance.Domain.Entities;
using Barion.Balance.Domain.Services.ServiceResponses;

namespace Barion.Balance.Domain.Services;

public interface IPaymentSystemService
{
    
    /// <summary>
    /// Получить идентификатор виджета из ответа платежной системы
    /// </summary>
    /// <param name="jsonResponse">Ответ платежной системы</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> GetWidgetId(string jsonResponse, 
        CancellationToken cancellationToken);
    
    /// <summary>
    /// Сделать запрос на платежную систему и создать виджет
    /// </summary>
    /// <param name="paymentSystemWidgetGeneration"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> GeneratePaymentSystemWidget(PaymentSystemWidgetGeneration paymentSystemWidgetGeneration,
        CancellationToken cancellationToken);

    /// <summary>
    /// Обработать веб-хук запрос после отправки формы на виджете платежной системы
    /// </summary>
    /// <param name="jsonResponse"></param>
    /// <param name="widgetGeneration"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ProcessCreatePaymentMethodPaymentSystemWidgetResult> ProcessCreatePaymentMethodPaymentSystemWidgetResponse(string jsonResponse,
        PaymentSystemWidgetGeneration widgetGeneration,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Захолдировать сумму
    /// </summary>
    /// <param name="hold"></param>
    /// <param name="paymentMethod"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ProcessHoldPaymentSystemResult> Hold(Hold hold,
        PaymentMethod paymentMethod,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Удержать сумму холда
    /// </summary>
    /// <param name="hold"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> CaptureHold(Hold hold, 
        CancellationToken cancellationToken);

    
    /// <summary>
    /// Вернуть сумму холда
    /// </summary>
    /// <param name="hold"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> VoidHold(Hold hold,
        CancellationToken cancellationToken);
}