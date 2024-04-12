using Barion.Balance.Domain.Enums;
using MediatR;

namespace Barion.Balance.Domain.Events.Holds;

public record MakeHoldEvent : INotification
{
    public required string UserId { get; set; }
    
    public required int PaidResourceId { get; set; }
    
    public required int OperatorId { get; set; }
    
    public required decimal Amount { get; set; }

    public required PaidResourceType PaidResourceType { get; set; }
    
    public required int PaymentMethodId { get; set; }
}