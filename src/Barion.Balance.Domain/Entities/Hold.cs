using System.ComponentModel.DataAnnotations.Schema;
using Barion.Balance.Domain.Common;
using Barion.Balance.Domain.Enums;

namespace Barion.Balance.Domain.Entities;

public class Hold : BaseAuditableEntity
{
    /// <summary>
    /// Identifier of user
    /// </summary>
    public required string UserId { get; set; }
    
    /// <summary>
    /// Тип платного ресурса
    /// </summary>
    public int PaidResourceTypeId { get; set; }

    public PaidResourceType PaidResourceType { get; set; }

    public required string PaidResourceId { get; set; }

    /// <summary>
    /// Id в платежной системе
    /// </summary>
    public required string? PaymentSystemTransactionId { get; set; }
    
    
    /// <summary>
    /// Id того, кто является получателем суммы платежа(продавец, оператор и тд.)
    /// </summary>
    public required string OperatorId { get; set; }
    
    /// <summary>
    /// Сумма
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Была ли удрержана сумма
    /// </summary>
    public bool IsCaptured { get; set; } 
    
    /// <summary>
    /// Была ли сумма отдана 
    /// </summary>
    public bool IsVoided { get; set; } 
    
    /// <summary>
    /// Id платежного метода
    /// </summary>
    public required int PaymentMethodId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    
    
    [Column(TypeName = "jsonb")]
    public string? AdditionalData { get; set; }

    
    public required string ReceiptUrl { get; set; }
}