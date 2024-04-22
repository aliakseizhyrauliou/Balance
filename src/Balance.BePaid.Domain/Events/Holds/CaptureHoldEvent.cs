using Balance.BePaid.Domain.Common;
using Balance.BePaid.Domain.Entities;

namespace Balance.BePaid.Domain.Events.Holds;

public class CaptureHoldEvent(Hold hold) : BaseEvent
{
    public Hold Hold { get; set; } = hold;
}