using System.Text.Json.Serialization;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.PaymentMethod;

public class PaymentMethod
{
    [JsonPropertyName("excluded_types")]
    public required List<string> ExcludedTypes { get; set; }
}