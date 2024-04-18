using Barion.Balance.Domain.Common;
using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Domain.Events.PaymentSystemWidgetGenerations;

public class PaymentSystemWidgetGenerationCreatedEvent(PaymentSystemWidget paymentSystemWidget)
    : BaseEvent
{
    public PaymentSystemWidget PaymentSystemWidget { get; set; } = paymentSystemWidget;
}
