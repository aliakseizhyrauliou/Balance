using Barion.Balance.Domain.Common;
using Barion.Balance.Domain.Enums;

namespace Barion.Balance.Domain.Entities;

public class PaymentMethod : BaseAuditableEntity
{
    /// <summary>
    /// Описание варианта оплаты
    /// </summary>
    public string? Description { get; set; }
    

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Первые 6 знаков карты
    /// </summary>
    public string? First1 { get; set; }

    /// <summary>
    /// Последние 4 символа карты
    /// </summary>
    public string? Last4 { get; set; }

    /// <summary>
    /// Тип карты (виза, мир итд)
    /// </summary>
    public BankCardType CardType { get; set; }

    /// <summary>
    /// Действует до: год
    /// </summary>
    public string? ExpiryYear { get; set; }

    /// <summary>
    /// Действует до: месяц
    /// </summary>
    public string? ExpiryMonth { get; set; }
    

    /// <summary>
    /// Is selected by user as payment method
    /// </summary>
    public bool IsSelected { get; set; }

    public string? PaymentSystemStamp { get; set; }
    
    public string? PaymentSystemToken { get; set; }

    public bool IsVerifiedByPaymentSystem { get; set; }
}