using Balance.BePaid.Domain.Common;
using Balance.BePaid.Domain.Entities;

namespace Balance.BePaid.Domain.Events.Payments;

public class CreatePaymentEvent(Payment payment) : BaseEvent
{
    public Payment Payment { get; set; } = payment;
}