using Newtonsoft.Json;

namespace Balance.BePaid.Infrastructure.External.BePaid.BePaidModels.Checkout.Request.Settings;

public class SaveCardToggle
{
    [JsonProperty(PropertyName = "display")] 
    public required bool Display { get; set; }

    [JsonProperty(PropertyName = "customer_contract")] 
    public required bool CustomerContract { get; set; }
}