using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction
{
    public class Transaction
    {
        [JsonProperty(PropertyName = "uid")]
        public required string? Id { get; set; }

        [JsonProperty(PropertyName = "status")]
        public required string Status { get; set; }

        [JsonProperty(PropertyName = "message")]
        public required string Message { get; set; }

        [JsonProperty(PropertyName = "tracking_id")]
        public required string TrackingId { get; set; }

        [JsonProperty(PropertyName = "language")]
        public required string Language { get; set; }

        [JsonProperty(PropertyName = "type")]
        public required string Type { get; set; }

        [JsonProperty(PropertyName = "payment_method_type")]
        public required string PaymentMethodType {  get; set; }

        [JsonProperty(PropertyName = "credit_card")]
        public CreditCard.CreditCard CreditCard { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }

        [JsonProperty(PropertyName = "receipt_url")]
        public string ReceiptUrl { get; set; }

    }
}
