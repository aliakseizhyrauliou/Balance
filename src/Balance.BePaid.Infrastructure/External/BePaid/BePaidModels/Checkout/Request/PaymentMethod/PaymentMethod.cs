using Newtonsoft.Json;

namespace Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.PaymentMethod;

public class PaymentMethod
{
    [JsonProperty(PropertyName = "excluded_types")]
    public required List<string> ExcludedTypes { get; set; }
}