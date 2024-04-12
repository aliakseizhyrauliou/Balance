using Newtonsoft.Json;

namespace Barion.Balance.Infrastructure.External.BePaid.BePaidModels;

public class OrderDto
{
    [JsonProperty(PropertyName = "currency")]
    public string Currency { get; set; }
    
    [JsonProperty(PropertyName= "amount")]
    public int Amount { get; set; }
    
    [JsonProperty(PropertyName= "description")]
    public string Description { get; set; }
    
    [JsonProperty(PropertyName = "additional_data")]
    public AdditionalDataDto AdditionalData { get; set; }

    [JsonProperty(PropertyName = "tracking_id")] 
    public string TrackingId { get; set; }
}