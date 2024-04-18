using Barion.Balance.Domain.Common;

namespace Barion.Balance.Domain.Entities;

public class Receipt : BaseAuditableEntity
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public required string UserId { get; set; }
    
    /// <summary>
    /// Id транзакции в платежной системе
    /// </summary>
    public required string? PaymentSystemTransactionId { get; set; } 
    
    /// <summary>
    /// Id того, за что платим
    /// </summary>
    public required string PaidResourceId { get; set; } = null!;

    /// <summary>
    /// Ссылка на чек
    /// </summary>
    public string Url { get; set; } = null!;
    
    /// <summary>
    /// Инентефикатор конфигурации платежной системы
    /// </summary>
    public int? PaymentSystemConfigurationId { get; set; }
    public PaymentSystemConfiguration? PaymentSystemConfiguration { get; set; }

    
    /// <summary>
    /// Карта
    /// </summary>
    public int? PaymentMethodId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }


    /// <summary>
    /// Платеж
    /// </summary>
    public int? PaymentId { get; set; }
    public Payment? Payment { get; set; }

    /// <summary>
    /// Холд
    /// </summary>
    public int? HoldId { get; set; }
    public Hold? Hold { get; set; }

    

    public bool IsReceiptForHold { get; set; }
    public bool IsReceiptForPayment { get; set; }
}