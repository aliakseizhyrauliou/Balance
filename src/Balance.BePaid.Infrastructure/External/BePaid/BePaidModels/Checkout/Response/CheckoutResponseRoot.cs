using Newtonsoft.Json;

namespace Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Checkout.Response;

public class CheckoutResponseRoot
{
    [JsonProperty(PropertyName = "checkout")]
    public CheckoutResponse CheckoutResponse { get; set; } = null!;
    
}