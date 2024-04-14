using Barion.Balance.Domain.Common;
using Barion.Balance.Domain.Enums;

namespace Barion.Balance.Domain.Entities;

public class PaymentSystemWidgetGeneration : BaseAuditableEntity
{
    public required string? UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public WidgetReason WidgetReason { get; set; }
    public decimal Amount { get; set; }
    public int? TrackingId { get; set; }
    public bool IsSuccess { get; set; }
    public required int PaymentSystemConfigurationId { get; set; }
    public PaymentSystemConfiguration? PaymentSystemConfiguration { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsDisabled { get; set; }
    public string? Url { get; set; }
    public string? Token { get; set; }
}