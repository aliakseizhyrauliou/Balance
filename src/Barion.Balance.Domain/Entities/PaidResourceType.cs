using Barion.Balance.Domain.Common;

namespace Barion.Balance.Domain.Entities;

public class PaidResourceType : BaseAuditableEntity
{
    public required string TypeName { get; set; }

    public ICollection<Hold>? Holds { get; set; }

    public ICollection<Payment>? Payments { get; set; }

    public ICollection<PaymentSystemWidgetGeneration>? PaymentSystemWidgetGenerations { get; set; }
    
}