using System.Text.Json.Serialization;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.Settings;

public class CustomerFields
{
    [JsonPropertyName("visible")]
    public required List<string> Visible { get; set; }

    [JsonPropertyName("read_only")]
    public required List<string> ReadOnly { get; set; }
}