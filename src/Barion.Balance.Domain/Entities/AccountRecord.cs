using Barion.Balance.Domain.Common;
using Barion.Balance.Domain.Enums;

namespace Barion.Balance.Domain.Entities;

public class AccountRecord : BaseAuditableEntity
{
    /// <summary>
    /// Идентификатор пользователя 
    /// </summary>
    public string UserId { get; set; }

    public string TagId { get; set; }

    /// <summary>
    /// Сумма платежа
    /// </summary>
    public decimal Cost { get; set; }
        
    /// <summary>
    ///Id того, за что была оплата
    ///Id бронирования, зарядки или парковки
    /// </summary>
    public int PaidResourceId { get; set; }

    /// <summary>
    /// Идентификатор платежа в платежной системе
    /// </summary>
    public string PaymentSystemFinancialTransactionId { get; set; }
        
    /// <summary>
    /// Тип платного ресурса
    /// </summary>
    public PaidResourceType PaidResourceType { get; set; }

    /// <summary>
    /// Идентификтор коннектора если платежный ресурс - зарядка
    /// </summary>
    public int? ConnectorId { get; set; }

    /// <summary>
    /// идентификтор зу если платный ресурс - зарядка
    /// </summary>
    public int? ChargerId { get; set; }

    /// <summary>
    /// Идентификатор тарифа
    /// </summary>
    public int? TariffId { get; set; }
        
    /// <summary>
    /// Идентификатор оператора
    /// </summary>
    public int? OperatorId { get; set; }
    
    /// <summary>
    /// Успешная ли транзакция
    /// </summary>
        
    public bool IsSuccessfully { get; set; }

        
    /// <summary>
    /// Идентификатор карты
    /// </summary>
    public int PaymentMethodId { get; set; }

    public string? Last4 { get; set; }

    public bool IsBonus { get; set; }
}