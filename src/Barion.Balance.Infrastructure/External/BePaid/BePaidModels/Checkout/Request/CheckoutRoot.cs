using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Checkout.Request
{
    public class CheckoutRoot
    {
        [JsonProperty(PropertyName = "checkout")]
        public required Checkout Checkout { get; set; }
    }
}
