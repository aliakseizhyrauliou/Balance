using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels
{
    public class CheckouRootDto
    {
        [JsonProperty(PropertyName = "checkout")]
        public CheckoutDto Checkout { get; set; } = new CheckoutDto();
    }
}
