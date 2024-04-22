using Balance.BePaid.Domain.Events.PaymentMethods;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Balance.BePaid.Application.PaymentMethods.EventHandlers;

public class PaymentMethodCreatedEventHandler(ILogger<PaymentMethodCreatedEventHandler> logger)
    : INotificationHandler<PaymentMethodCreatedEvent>
{
    public Task Handle(PaymentMethodCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogWarning("PaymentMethodCreatedEventHandler");
        return Task.CompletedTask;
    }
}