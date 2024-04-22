using Balance.BePaid.Domain.Common;
using Balance.BePaid.Domain.Enums;

namespace Balance.BePaid.Domain.Entities;

public class PaymentMethod : BaseAuditableEntity
{
    /// <summary>
    /// Описание варианта оплаты
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Первый символ карты
    /// </summary>
    public string? First1 { get; set; }

    /// <summary>
    /// Последние 4 символа карты
    /// </summary>
    public string? Last4 { get; set; }

    /// <summary>
    /// В случае, если имеем доступ к другим символам
    /// </summary>
    public required string CardNumberData { get; set; }

    /// <summary>
    /// Тип карты (виза, мир итд)
    /// </summary>
    public BankCardType CardType { get; set; }

    /// <summary>
    /// Действует до: год
    /// </summary>
    public required int ExpiryYear { get; set; }

    /// <summary>
    /// Действует до: месяц
    /// </summary>
    public required int ExpiryMonth { get; set; }
    
    /// <summary>
    /// Is selected by user as payment method
    /// </summary>
    public bool IsSelected { get; set; }
    
    public string? PaymentSystemStamp { get; set; }
    
    /// <summary>
    /// Токен платежной системы
    /// </summary>
    public string? PaymentSystemToken { get; set; }


    public ICollection<Receipt>? Receipts { get; set; }
    public ICollection<Hold>? Holds { get; set; }
    public ICollection<Payment>? Payments { get; set; }
    
    public ICollection<Debtor>? Debtors { get; set; }
}