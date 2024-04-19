using System.Collections;
using Barion.Balance.Domain.Common;

namespace Barion.Balance.Domain.Entities;

public class PaidResourceType : BaseAuditableEntity
{
    public required string TypeName { get; set; }

    public ICollection<Hold>? Holds { get; set; }

    public ICollection<Payment>? Payments { get; set; }

    public ICollection<PaymentSystemWidget>? PaymentSystemWidgets { get; set; }

    public ICollection<Debtor> Debtors { get; set; }

}