using Barion.Balance.Domain.Common;

namespace Barion.Balance.Domain.Entities;

public class Debtor : BaseAuditableEntity
{
    public string? UserId { get; set; }

    /// <summary>
    /// Карта
    /// </summary>
    public int? PaymentMethodId { get; set; }

    public PaymentMethod? PaymentMethod { get; set; }

    /// <summary>
    /// Тип платного ресурса
    /// </summary>
    public int? PaidResourceTypeId { get; set; }

    public PaidResourceType? PaidResourceType { get; set; }

    /// <summary>
    /// Платежная система
    /// </summary>
    public int? PaymentSystemConfigurationId { get; set; }

    public PaymentSystemConfiguration PaymentSystemConfiguration { get; set; }

    public required string OperatorId { get; set; }
    public string? PaidResourceId { get; set; }

    public int? NewPaymentId { get; set; }
    public Payment? NewPayment { get; set; }

    public string? AdditionalData { get; set; }

    public int CaptureAttemptCount { get; set; }
    public DateTime? LastAttempt { get; set; }
    
}