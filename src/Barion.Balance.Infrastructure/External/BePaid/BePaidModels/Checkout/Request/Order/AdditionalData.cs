using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.Order;

public class AdditionalData
{
    [JsonProperty(PropertyName = "contract")]
    public required List<string>? Contract { get; set; }

    [JsonPropertyName("receipt_text")]
    public required List<string> ReceiptText { get; set; }
}