using Balance.BePaid.Domain.Common;
using Balance.BePaid.Domain.Entities;

namespace Balance.BePaid.Domain.Events.PaymentSystemWidgetGenerations;

public class PaymentSystemWidgetGenerationCreatedEvent(PaymentSystemWidget paymentSystemWidget)
    : BaseEvent
{
    public PaymentSystemWidget PaymentSystemWidget { get; set; } = paymentSystemWidget;
}
