using Barion.Balance.Domain.Common;
using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Domain.Events.PaymentMethods;

public class PaymentMethodSelectedEvent(PaymentMethod paymentMethod) 
    : BaseEvent
{
    public PaymentMethod PaymentMethod { get; set; } = paymentMethod;
}