using Barion.Balance.Domain.Common;
using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Domain.Events.PaymentSystemWidgetGenerations;

public class PaymentSystemWidgetGenerationCreatedEvent(PaymentSystemWidgetGeneration paymentSystemWidgetGeneration)
    : BaseEvent
{
    public PaymentSystemWidgetGeneration PaymentSystemWidgetGeneration { get; set; } = paymentSystemWidgetGeneration;
}
