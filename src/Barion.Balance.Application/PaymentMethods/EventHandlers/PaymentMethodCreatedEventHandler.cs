using Barion.Balance.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barion.Balance.Application.PaymentMethods.EventHandlers;

public class PaymentMethodCreatedEventHandler(ILogger<PaymentMethodCreatedEventHandler> logger)
    : INotificationHandler<PaymentMethodCreatedEvent>
{
    public Task Handle(PaymentMethodCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogWarning("PaymentMethodCreatedEventHandler");
        return Task.CompletedTask;
    }
}