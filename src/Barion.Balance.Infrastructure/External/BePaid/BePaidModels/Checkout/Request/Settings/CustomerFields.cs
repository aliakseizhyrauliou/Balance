using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.Settings;

public class CustomerFields
{
    [JsonProperty(PropertyName = "visible")]
    public required List<string> Visible { get; set; }

    [JsonProperty(PropertyName = "read_only")]
    public required List<string> ReadOnly { get; set; }
}