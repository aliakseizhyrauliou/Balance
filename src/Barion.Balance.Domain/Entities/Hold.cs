using Barion.Balance.Domain.Common;
using Barion.Balance.Domain.Enums;

namespace Barion.Balance.Domain.Entities;

public class Hold : BaseAuditableEntity
{
    /// <summary>
    /// Identifier of user
    /// </summary>
    public string UserId { get; set; }
    
    public int? PaidResourceId { get; set; }
    
    public string? PaymentSystemTransactionId { get; set; }
    
    public int? OperatorId { get; set; }
    
    public decimal Amount { get; set; }
    
    public bool IsCaptured { get; set; } 
    
    public bool IsVoided { get; set; } 

    public PaidResourceType PaidResourceType { get; set; }
    
    public int PaymentMethodId { get; set; }
}