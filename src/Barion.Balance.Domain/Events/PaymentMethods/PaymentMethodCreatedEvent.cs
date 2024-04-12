using Barion.Balance.Domain.Common;
using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Domain.Events;

public class PaymentMethodCreatedEvent : BaseEvent
{
    public PaymentMethodCreatedEvent(PaymentMethod paymentMethod)
    {
        
    }

    public PaymentMethod PaymentMethod { get; set; }
}