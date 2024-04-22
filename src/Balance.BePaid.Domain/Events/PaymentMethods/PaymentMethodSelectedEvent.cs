using Balance.BePaid.Domain.Common;
using Balance.BePaid.Domain.Entities;

namespace Balance.BePaid.Domain.Events.PaymentMethods;

public class PaymentMethodSelectedEvent(PaymentMethod paymentMethod) 
    : BaseEvent
{
    public PaymentMethod PaymentMethod { get; set; } = paymentMethod;
}