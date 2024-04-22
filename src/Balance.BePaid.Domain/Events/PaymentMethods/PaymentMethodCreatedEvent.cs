using Balance.BePaid.Domain.Common;
using Balance.BePaid.Domain.Entities;

namespace Balance.BePaid.Domain.Events.PaymentMethods;

public class PaymentMethodCreatedEvent : BaseEvent
{
    public PaymentMethodCreatedEvent(PaymentMethod paymentMethod)
    {
        
    }

    public PaymentMethod PaymentMethod { get; set; }
}