using Barion.Balance.Domain.Common;
using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Domain.Events.Payments;

public class CreatePaymentEvent(Payment payment) : BaseEvent
{
    public Payment Payment { get; set; } = payment;
}