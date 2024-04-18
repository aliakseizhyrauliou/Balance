using Barion.Balance.Domain.Common;
using Barion.Balance.Domain.Entities;

namespace Barion.Balance.Domain.Events.Holds;

public class VoidHoldEvent(Hold hold) : BaseEvent
{
    public Hold Hold { get; set; } = hold;
}