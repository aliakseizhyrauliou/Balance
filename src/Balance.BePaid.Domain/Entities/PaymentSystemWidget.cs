using System.ComponentModel.DataAnnotations.Schema;
using Balance.BePaid.Domain.Common;
using Balance.BePaid.Domain.Enums;

namespace Balance.BePaid.Domain.Entities;

public class PaymentSystemWidget : BaseAuditableEntity
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
    ///Id того, за что была оплата
    ///Id бронирования, зарядки или парковки
    /// </summary>
    public required string PaidResourceId { get; set; }

    /// <summary>
    /// Сумма
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Успешно ли создание виджета.
    /// Имеется в виду не ответ от платежной системы, а сам факт создания виджета
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
    public int PaymentSystemConfigurationId { get; set; }

    public PaymentSystemConfiguration? PaymentSystemConfiguration { get; set; }

    /// <summary>
    /// Тип платного ресурса
    /// </summary>
    public int PaidResourceTypeId { get; set; }

    public PaidResourceType? PaidResourceType { get; set; }

    /// <summary>
    /// Id холда если есть 
    /// </summary>
    public int? HoldId { get; set; }

    public Hold? Hold { get; set; }

    /// <summary>
    /// Id платежа
    /// </summary>
    public int? PaymentId { get; set; }

    public Payment? Payment { get; set; }

    /// <summary>
    /// Получатель суммы транзакции
    /// </summary>
    public required string OperatorId { get; set; }

    /// <summary>
    /// Дополнительная информация
    /// </summary>
    [Column(TypeName = "jsonb")]
    public string? AdditionalData { get; set; }

    /// <summary>
    /// Ошибка от платежной системы если есть
    /// </summary>
    public string? PaymentSystemMessage { get; set; }
}