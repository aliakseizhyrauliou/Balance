using Balance.BePaid.Application.Common.Repositories;
using Balance.BePaid.Domain.Events.PaymentSystemWidgetGenerations;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Balance.BePaid.Application.PaymentSystemWidgets.EventHandlers;

public class PaymentSystemWidgetCreatedEventHandler(ILogger<PaymentSystemWidgetCreatedEventHandler> logger, 
    IPaymentSystemWidgetGenerationRepository repository) 
    : INotificationHandler<PaymentSystemWidgetGenerationCreatedEvent>
{
    /// <summary>
    /// Перед созданием нового виджета, нужно отключить все старые. У пользователя может быть только один активный виджет
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task Handle(PaymentSystemWidgetGenerationCreatedEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            await repository.DisableAllUserWidgetsAsync(notification.PaymentSystemWidget.UserId,
                cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogCritical($"Erorr while DisableAllUserWidgetsAsync. Error message = {ex.Message}");
        }

    }
}