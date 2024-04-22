using Newtonsoft.Json;

namespace Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Checkout.Request
{
    public class CheckoutRoot
    {
        [JsonProperty(PropertyName = "checkout")]
        public required Checkout Checkout { get; set; }
    }
}
