using System.ComponentModel.DataAnnotations.Schema;
using Balance.BePaid.Domain.Common;

namespace Balance.BePaid.Domain.Entities;

public class Hold : BaseAuditableEntity
{
    /// <summary>
    /// Identifier of user
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Тип платного ресурса
    /// </summary>
    public int? PaidResourceTypeId { get; set; }

    public PaidResourceType? PaidResourceType { get; set; }

    /// <summary>
    /// Id того, за что платим
    /// </summary>
    public required string PaidResourceId { get; set; }


    /// <summary>
    /// Id в платежной системе
    /// </summary>
    public string PaymentSystemTransactionId { get; set; }


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
    public int? PaymentMethodId { get; set; }

    public PaymentMethod? PaymentMethod { get; set; }


    /// <summary>
    /// Дополнительная информация
    /// </summary>
    [Column(TypeName = "jsonb")]
    public string? AdditionalData { get; set; }

    /// <summary>
    /// Id конфигурации платежной системы
    /// </summary>
    public required int PaymentSystemConfigurationId { get; set; }

    public PaymentSystemConfiguration PaymentSystemConfiguration { get; set; }

    public ICollection<Receipt>? Receipts { get; set; } = new List<Receipt>();

    /// <summary>
    /// Id чека
    /// </summary>
    public string ReceiptUrl { get; set; } = null!;

    /// <summary>
    /// Id платежа который создало удержание данного холда
    /// </summary>
    public int? HoldCaptureCreatedPaymentId { get; set; }
    public Payment?  HoldCaptureCreatedPayment { get; set; }
    
    
    /// <summary>
    /// Идентификатор платежного виджета если есть
    /// </summary>
    public int? PaymentSystemWidgetGenerationId { get; set; }
    
    public PaymentSystemWidget? PaymentSystemWidget { get; set; }
}