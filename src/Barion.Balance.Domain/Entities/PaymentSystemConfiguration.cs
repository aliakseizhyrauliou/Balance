using System.ComponentModel.DataAnnotations.Schema;
using Barion.Balance.Domain.Common;

namespace Barion.Balance.Domain.Entities;

public class PaymentSystemConfiguration : BaseAuditableEntity
{
    public required string PaymentSystemName { get; set; } 
    
    [Column(TypeName = "jsonb")]
    public required string Data { get; set; }
}