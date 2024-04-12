using Barion.Balance.Domain.Events.PaymentMethods;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barion.Balance.Application.PaymentMethods.EventHandlers;

public sealed class PaymentMethodSelectedEventHandler(ILogger<PaymentMethodCreatedEventHandler> logger)
    : INotificationHandler<PaymentMethodSelectedEvent>
{
    public Task Handle(PaymentMethodSelectedEvent notification, CancellationToken cancellationToken)
    {

        return Task.CompletedTask;
    }
}