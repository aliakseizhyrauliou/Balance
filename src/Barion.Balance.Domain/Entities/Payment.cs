using System.ComponentModel.DataAnnotations.Schema;
using Barion.Balance.Domain.Common;

namespace Barion.Balance.Domain.Entities;

public class Payment : BaseAuditableEntity
{
    /// <summary>
    /// Идентификатор пользователя 
    /// </summary>
    public required string UserId { get; set; }
    
    /// <summary>
    /// Сумма платежа
    /// </summary>
    public required decimal Amount { get; set; }
        
    /// <summary>
    ///Id того, за что была оплата
    ///Id бронирования, зарядки или парковки
    /// </summary>
    public required string PaidResourceId { get; set; }

    /// <summary>
    /// Идентификатор платежа в платежной системе
    /// </summary>
    public required string PaymentSystemFinancialTransactionId { get; set; } 

    /// <summary>
    /// Специфическая инфа платежа.
    /// </summary>
    [Column(TypeName = "jsonb")]
    public string? AdditionalData { get; set; }
    
    
    /// <summary>
    /// Получатель суммы транзакции
    /// </summary>
    public required string OperatorId { get; set; }
        
    /// <summary>
    /// Успешная ли транзакция
    /// </summary>
    public bool IsSuccess { get; set; }
    
    /// <summary>
    /// Была ли оплата за счет бонусов
    /// </summary>
    public bool IsBonus { get; set; }
    
    /// <summary>
    /// Id платежного метода
    /// </summary>
    public required int PaymentMethodId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    public required int PaidResourceTypeId { get; set; }
    public PaidResourceType PaidResourceType { get; set; }
    
    public required string ReceiptUrl { get; set; }
}