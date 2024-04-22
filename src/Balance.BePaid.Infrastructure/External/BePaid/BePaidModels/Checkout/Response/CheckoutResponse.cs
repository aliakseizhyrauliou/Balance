using Newtonsoft.Json;

namespace Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Checkout.Response;

    public class CheckoutResponse
    {
        [JsonProperty(PropertyName = "token")]
        public required string Token { get; set; }
        
        [JsonProperty(PropertyName = "redirect_url")]
        public required string RedirectUrl { get; set; }
    }