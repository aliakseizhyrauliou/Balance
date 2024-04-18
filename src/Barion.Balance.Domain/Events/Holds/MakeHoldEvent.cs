using Barion.Balance.Domain.Common;
using Barion.Balance.Domain.Entities;
using MediatR;

namespace Barion.Balance.Domain.Events.Holds;

public class MakeHoldEvent(Hold hold) : BaseEvent
{
    public Hold Hold { get; set; } = hold;
}