using Barion.Balance.Domain.Common;
using Barion.Balance.Domain.Enums;

namespace Barion.Balance.Domain.Entities;

public class PaymentSystemWidgetGeneration : BaseAuditableEntity
{
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    public required string UserId { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string FirstName { get; set; }
    
    /// <summary>
    /// Имя пользовтеля
    /// </summary>
    public required string LastName { get; set; }
    
    /// <summary>
    /// Причина открытия виджета
    /// </summary>
    public WidgetReason WidgetReason { get; set; }
    
    /// <summary>
    /// Сумма
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Успешно ли открытие виджета
    /// </summary>
    public bool IsSuccess { get; set; }
    
    /// <summary>
    /// Получили ли ответ от платежной системы
    /// </summary>
    public bool GotResponseFromPaymentSystem { get; set; }
    
    /// <summary>
    /// Активен ли виджет
    /// </summary>
    public bool IsDisabled { get; set; }
    
    /// <summary>
    /// Адрес виджета
    /// </summary>
    public string? Url { get; set; }
    
    /// <summary>
    /// Токен виджета
    /// </summary>
    public string? Token { get; set; }
    
    /// <summary>
    /// Платежная система
    /// </summary>
    public int? PaymentSystemConfigurationId { get; set; }
    public PaymentSystemConfiguration? PaymentSystemConfiguration { get; set; }

    /// <summary>
    /// Тип платежного виджета
    /// </summary>
    public int? PaidResourceTypeId { get; set; }
    public PaidResourceType? PaidResourceType { get; set; }
}