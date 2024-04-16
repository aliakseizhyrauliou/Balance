using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction.CreditCard;
using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels.CreateTransaction;

public class CreateTransaction
{
    [JsonProperty(PropertyName = "amount")]
    public int Amount { get; set; }

    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }

    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; }

    [JsonProperty(PropertyName = "tracking_id")]
    public string TrackingId { get; set; }

    [JsonProperty(PropertyName = "expired_at")]
    public string ExpiredAt { get; set; }

    [JsonProperty(PropertyName = "duplicate_check")]
    public bool DuplicateCheck { get; set; }

    [JsonProperty(PropertyName = "language")]
    public string Language { get; set; }

    [JsonProperty(PropertyName = "notification_url")]
    public string? NotificationUrl { get; set; }

    [JsonProperty(PropertyName = "verification_url")]
    public string? VerificationUrl { get; set; }

    [JsonProperty(PropertyName = "return_url")]
    public string? ReturnUrl { get; set; }

    [JsonProperty(PropertyName = "test")]
    public bool Test { get; set; }

    [JsonProperty(PropertyName = "credit_card")]
    public CreateTransactionCreditCard CreditCard { get; set; }
}